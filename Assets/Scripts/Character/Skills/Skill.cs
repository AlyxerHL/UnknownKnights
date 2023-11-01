using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private float cooldown;

    private bool isCooldown = true;
    private CancellationTokenSource skillCancellation;
    private CancellationTokenSource autoSkillCancellation;

    public event Action Used;

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
        Used?.Invoke();
        Cooldown().Forget();
        skillCancellation = new();

        weapon.enabled = false;
        await UseInternal(skillCancellation.Token);
        weapon.enabled = true;
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
