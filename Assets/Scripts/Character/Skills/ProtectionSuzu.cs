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
        var effects = finder.Tags.Select((tag) => (tag, id: tag.Effector.SetDamageReduction(0f)));
        effects.ForEach((e) => e.tag.Health.GetHealed(HealAmount));
        await UniTask.Delay(EffectDuration, cancellationToken: cancellationToken);
        effects.ForEach((e) => e.tag.Effector.ClearDamageReduction(e.id));
    }
}
