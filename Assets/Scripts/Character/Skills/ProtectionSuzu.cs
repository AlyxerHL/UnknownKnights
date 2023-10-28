using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

public class ProtectionSuzu : Skill
{
    private const float HealAmount = 40f;
    private const int EffectDuration = 850;

    protected override bool CanUse => true;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        var tags = CharacterTag.ActiveTargetTags.Where((tag) => tag.IsFriendly);
        foreach (var tag in tags)
        {
            tag.Health.GetHealed(HealAmount);
            tag.Health.IsImmortal = true;
        }

        await UniTask.Delay(EffectDuration, cancellationToken: cancellationToken);
        tags.ForEach((tag) => tag.Health.IsImmortal = false);
    }
}
