using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Railgun : Skill
{
    private const float Damage = 135f;
    private const float Range = 40f;
    private const int RecoveryTime = 650;

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
