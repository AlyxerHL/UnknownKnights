using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DragonBlade : Skill
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float range;

    [SerializeField]
    private int recoveryTime;

    private bool IsWithinRange =>
        (transform.position - tagFinder.TargetTag.transform.position).sqrMagnitude <= range;

    public override async UniTask Use(CancellationToken cancellationToken)
    {
        if (IsWithinRange)
        {
            tagFinder.TargetTag.Health.GetDamaged(damage);
        }
        await UniTask.Delay(recoveryTime, cancellationToken: cancellationToken);
    }
}
