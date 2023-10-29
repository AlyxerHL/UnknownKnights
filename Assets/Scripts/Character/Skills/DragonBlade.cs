using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DragonBlade : Skill
{
    private const float Damage = 70f;
    private const float Range = 1f;
    private const int RecoveryTime = 900;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override bool CanUse =>
        finder.Tag != null
        && (transform.position - finder.Tag.transform.position).sqrMagnitude <= Range;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        finder.Tag.Health.GetDamaged(Damage);
        await UniTask.Delay(RecoveryTime, cancellationToken: cancellationToken);
    }
}
