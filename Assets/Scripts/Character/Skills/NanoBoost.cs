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
        var damageBuffID = finder.Tag.ApplyDamageBuff(2f);
        var damageReductionID = finder.Tag.ApplyDamageReduction(0.5f);
        await UniTask.Delay(EffectDuration, cancellationToken: cancellationToken);
        finder.Tag.RemoveDamageBuff(damageBuffID);
        finder.Tag.RemoveDamageReduction(damageReductionID);
    }
}
