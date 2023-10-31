using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Hack : Skill
{
    private const float Range = 15f;
    private const int EffectDuration = 1500;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override bool CanUse =>
        finder.Tag != null
        && (transform.position - finder.Tag.transform.position).sqrMagnitude <= Range;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        var effectID = finder.Tag.SetStun();
        await UniTask.Delay(EffectDuration, cancellationToken: cancellationToken);
        finder.Tag.ClearStun(effectID);
    }
}
