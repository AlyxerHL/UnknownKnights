using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DragonBlade : Skill
{
    private const float Damage = 70f;
    private const float Range = 1f;
    private const int RecoveryTime = 900;

    [SerializeField]
    private NearestEnemyFinder finder;

    private bool IsWithinRange =>
        (transform.position - finder.TargetTag.transform.position).sqrMagnitude <= Range;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        if (IsWithinRange)
        {
            finder.TargetTag.Health.GetDamaged(Damage);
        }
        await UniTask.Delay(RecoveryTime, cancellationToken: cancellationToken);
    }
}
