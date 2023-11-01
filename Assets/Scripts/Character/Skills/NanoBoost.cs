using Cysharp.Threading.Tasks;
using UnityEngine;

public class NanoBoost : Skill
{
    [SerializeField]
    private float damageBuff = 2f;

    [SerializeField]
    private float damageReduction = 0.5f;

    [SerializeField]
    private float effectDuration = 4f;

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
