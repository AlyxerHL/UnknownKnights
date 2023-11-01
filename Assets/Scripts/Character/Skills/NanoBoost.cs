using Cysharp.Threading.Tasks;
using UnityEngine;

public class NanoBoost : Skill
{
    [SerializeField]
    private float damageBuff;

    [SerializeField]
    private float damageReduction;

    [SerializeField]
    private float effectDuration;

    [SerializeField]
    private NearestFriendlyCharacterFinder finder;

    protected override bool CanUse => finder.Character != null;

    protected override UniTask ApplyEffect()
    {
        finder.Character.Effector.ApplyDamageBuff(damageBuff, effectDuration).Forget();
        finder.Character.Effector.ApplyDamageReduction(damageReduction, effectDuration).Forget();
        return UniTask.CompletedTask;
    }
}
