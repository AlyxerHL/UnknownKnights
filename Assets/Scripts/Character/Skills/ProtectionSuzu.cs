using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProtectionSuzu : Skill
{
    [SerializeField]
    private float healAmount = 40f;

    [SerializeField]
    private float effectDuration = 0.85f;

    [SerializeField]
    private FriendlyCharactersFinder finder;

    protected override UniTask Use(CancellationToken cancellationToken)
    {
        foreach (var character in finder.Characters)
        {
            character.Health.GetHealed(healAmount);
            character.Effector.ApplyDamageReduction(0f, effectDuration).Forget();
        }
        return UniTask.CompletedTask;
    }
}
