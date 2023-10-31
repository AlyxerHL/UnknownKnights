using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProtectionSuzu : Skill
{
    private const float HealAmount = 40f;
    private const int EffectDuration = 850;

    [SerializeField]
    private FriendlyCharactersFinder finder;

    protected override bool CanUse => true;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        var effects = finder.Tags.Select(
            (tag) =>
            {
                tag.Health.GetHealed(HealAmount);
                var id = tag.Effector.SetDamageReduction(0f);
                return (effecter: tag.Effector, id);
            }
        );

        await UniTask.Delay(EffectDuration, cancellationToken: cancellationToken);
        effects.ForEach((e) => e.effecter.ClearDamageReduction(e.id));
    }
}
