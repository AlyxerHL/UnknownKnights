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
        foreach (var tag in finder.Tags)
        {
            tag.Health.GetHealed(HealAmount);
            tag.Health.DamageRate = 0f;
        }

        await UniTask.Delay(EffectDuration, cancellationToken: cancellationToken);
        finder.Tags.ForEach((tag) => tag.Health.DamageRate = 1f);
    }
}
