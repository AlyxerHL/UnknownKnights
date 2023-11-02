using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [field: SerializeField]
    public string Quote { get; private set; }

    [SerializeField]
    private float cooldown;

    private bool isCooldown = true;
    private CancellationTokenSource autoSkillCancellation;
    private static readonly Queue<Skill> skillQueue = new();

    public event Func<UniTask> BeganUsing;
    public event Func<UniTask> EndedUsing;
    public event Action<float> CooldownBegan;

    protected abstract bool CanUse { get; }

    private void Start()
    {
        Cooldown().Forget();
        CooldownBegan?.Invoke(cooldown);
    }

    private void OnEnable()
    {
        StartAutoSkill().Forget();
    }

    private void OnDisable()
    {
        StopAutoSkill();
    }

    public async UniTask Use()
    {
        if (skillQueue.Count == 0)
        {
            Time.timeScale = 0f;
        }

        skillQueue.Enqueue(this);
        await UniTask.WaitUntil(() => skillQueue.Peek() == this);
        await Use();
        skillQueue.Dequeue();

        if (skillQueue.Count == 0)
        {
            Time.timeScale = 1f;
        }

        async UniTask Use()
        {
            await (BeganUsing?.Invoke() ?? UniTask.CompletedTask);
            Cooldown().Forget();
            CooldownBegan?.Invoke(cooldown);
            await ApplyEffect();
            await (EndedUsing?.Invoke() ?? UniTask.CompletedTask);
        }
    }

    public async UniTask StartAutoSkill()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

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

    private async UniTask Cooldown()
    {
        isCooldown = true;
        await UniTask.WaitForSeconds(cooldown);
        isCooldown = false;
    }
}
