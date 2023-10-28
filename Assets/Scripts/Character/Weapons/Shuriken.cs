using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Shuriken : Weapon
{
    private const float Damage = 10f;
    private const float Range = 4f;
    private const int RecoveryTime = 680;

    [SerializeField]
    private NearestEnemyFinder finder;

    private bool CanFire =>
        finder.TargetTag != null
        && (transform.position - finder.TargetTag.transform.position).sqrMagnitude <= Range;

    protected override async UniTask Fire(CancellationToken cancellationToken)
    {
        await UniTask.WaitUntil(() => CanFire, cancellationToken: cancellationToken);
        finder.TargetTag.Health.GetDamaged(Damage);
        await UniTask.Delay(RecoveryTime, cancellationToken: cancellationToken);
    }
}
