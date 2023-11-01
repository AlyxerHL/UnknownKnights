using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProtectionSuzu : Skill
{
    [SerializeField]
    private float healAmount = 40f;

    [SerializeField]
    private int effectDuration = 850;

    [SerializeField]
    private FriendlyCharactersFinder finder;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        var targets = finder.Tags.Select(
            (tag) =>
                (
                    health: tag.Health,
                    effector: tag.Effector,
                    effectID: tag.Effector.SetDamageReduction(0f)
                )
        );

        targets.Where((t) => t.health != null).ForEach((e) => e.health.GetHealed(healAmount));
        await UniTask.Delay(effectDuration, cancellationToken: cancellationToken);
        targets
            .Where((t) => t.effector != null)
            .ForEach((t) => t.effector.ClearDamageReduction(t.effectID));
    }
}
