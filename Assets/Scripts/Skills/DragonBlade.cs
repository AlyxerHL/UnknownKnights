using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DragonBlade : Skill
{
    [Header(nameof(DragonBlade))]
    [SerializeField]
    private float damage;

    [SerializeField]
    private float range;

    protected override bool CanUse =>
        tagFinder.TargetTag != null
        && (transform.position - tagFinder.TargetTag.transform.position).sqrMagnitude <= range;

    protected override UniTask Use(CancellationToken cancellationToken)
    {
        if (!CanUse)
        {
            return UniTask.CompletedTask;
        }

        tagFinder.TargetTag.Health.GetDamaged(damage);
        return UniTask.CompletedTask;
    }
}
