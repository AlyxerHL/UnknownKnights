using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Shuriken : Weapon
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float range;

    [SerializeField]
    private int recoveryTime;

    protected override bool CanFire =>
        tagFinder.TargetTag != null
        && (transform.position - tagFinder.TargetTag.transform.position).sqrMagnitude <= range;

    protected override async UniTask Fire(CancellationToken cancellationToken)
    {
        tagFinder.TargetTag.Health.GetDamaged(damage);
        await UniTask.Delay(recoveryTime, cancellationToken: cancellationToken);
    }
}
