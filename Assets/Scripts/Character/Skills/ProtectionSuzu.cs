using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProtectionSuzu : Skill
{
    [SerializeField]
    private float healAmount;

    [SerializeField]
    private float effectDuration;

    [SerializeField]
    private FriendlyCharactersFinder finder;

    protected override bool CanUse => true;

    protected override void ApplyEffect()
    {
        foreach (var character in finder.Characters)
        {
            character.Health.GetHealed(healAmount);
            character.Effector.Purify();
            character.Effector.ApplyDamageReduction(0f, effectDuration).Forget();
        }
    }
}
