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

    protected override UniTask Use(CancellationToken cancellationToken)
    {
        foreach (var tag in finder.Tags)
        {
            tag.Health.GetHealed(healAmount);
            tag.Effector.ApplyDamageReduction(0f, effectDuration).Forget();
        }
        return UniTask.CompletedTask;
    }
}
