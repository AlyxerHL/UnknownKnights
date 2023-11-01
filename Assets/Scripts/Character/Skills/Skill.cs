using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    private static readonly Queue<Skill> skillQueue = new();

    [SerializeField]
    private Weapon weapon;

    [field: SerializeField]
    public string Quote { get; private set; }

    [SerializeField]
    private float cooldown;

    private bool isCooldown = true;
    private CancellationTokenSource skillCancellation;
    private CancellationTokenSource autoSkillCancellation;

    public event Func<UniTask> BeganUsing;
    public event Func<UniTask> EndedUsing;

    private void Start()
    {
        Cooldown().Forget();
        StartAutoSkill().Forget();
    }

    private void OnDisable()
    {
        Cancel();
        StopAutoSkill();
    }

    public async UniTask Cooldown()
    {
        isCooldown = true;
        await UniTask.WaitForSeconds(cooldown);
        isCooldown = false;
    }

    public async UniTask Use()
    {
        skillQueue.Enqueue(this);
        await UniTask.WaitUntil(() => skillQueue.Peek() == this);
        await Use();
        skillQueue.Dequeue();

        async UniTask Use()
        {
            Time.timeScale = 0f;
            weapon.enabled = false;
            await (BeganUsing?.Invoke() ?? UniTask.CompletedTask);

            Cooldown().Forget();
            skillCancellation = new();
            await UseInternal(skillCancellation.Token);

            await (EndedUsing?.Invoke() ?? UniTask.CompletedTask);
            weapon.enabled = true;
            Time.timeScale = 1f;
        }
    }

    public void Cancel()
    {
        skillCancellation?.Cancel();
    }

    public async UniTask StartAutoSkill()
    {
        autoSkillCancellation = new();

        while (!autoSkillCancellation.Token.IsCancellationRequested)
        {
            await UniTask.WaitWhile(
                () => isCooldown,
                cancellationToken: autoSkillCancellation.Token
            );

            await Use();
        }
    }

    public void StopAutoSkill()
    {
        autoSkillCancellation?.Cancel();
    }

    protected abstract UniTask UseInternal(CancellationToken cancellationToken);
}
