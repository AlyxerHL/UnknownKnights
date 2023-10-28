using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Deadeye : Skill
{
    private const float Damage = 250f;
    private const int AimingTime = 3000;
    private const int RecoveryTime = 300;

    [SerializeField]
    private NearestEnemyFinder finder;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        await UniTask.Delay(AimingTime, cancellationToken: cancellationToken);
        finder.TargetTag.Health.GetDamaged(Damage);
        await UniTask.Delay(RecoveryTime, cancellationToken: cancellationToken);
    }
}
