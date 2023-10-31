using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NanoBoost : Skill
{
    private const int EffectDuration = 4000;

    [SerializeField]
    private NearestFriendlyCharacterFinder finder;

    protected override bool CanUse => true;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        var damageBuffID = finder.Tag.SetDamageBuff(2f);
        var damageReductionID = finder.Tag.SetDamageReduction(0.5f);
        await UniTask.Delay(EffectDuration, cancellationToken: cancellationToken);
        finder.Tag.ClearDamageBuff(damageBuffID);
        finder.Tag.ClearDamageReduction(damageReductionID);
    }
}
