using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    [field: SerializeField]
    public string Quote { get; private set; }

    [SerializeField]
    private float cooldown;

    private bool isCooldown = true;
    private CancellationTokenSource autoSkillCancellation;
    private static readonly Queue<Skill> skillQueue = new();

    public event Func<UniTask> BeganUsing;
    public event Func<UniTask> EndedUsing;

    protected abstract bool CanUse { get; }

    private void Start()
    {
        Cooldown().Forget();
    }

    private void OnEnable()
    {
        StartAutoSkill().Forget();
    }

    private void OnDisable()
    {
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
            await ApplyEffect();

            await (EndedUsing?.Invoke() ?? UniTask.CompletedTask);
            weapon.enabled = true;
            Time.timeScale = 1f;
        }
    }

    public async UniTask StartAutoSkill()
    {
        autoSkillCancellation = new();

        while (!autoSkillCancellation.Token.IsCancellationRequested)
        {
            await UniTask.WaitWhile(
                () => isCooldown || !CanUse,
                cancellationToken: autoSkillCancellation.Token
            );
            await Use();
        }
    }

    public void StopAutoSkill()
    {
        autoSkillCancellation?.Cancel();
    }

    protected abstract UniTask ApplyEffect();
}
