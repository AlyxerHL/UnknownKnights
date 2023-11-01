using System.Threading;
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

    protected override UniTask Use(CancellationToken cancellationToken)
    {
        if (finder.Tag == null)
        {
            return UniTask.CompletedTask;
        }

        finder.Tag.Effector.ApplyDamageBuff(damageBuff, effectDuration).Forget();
        finder.Tag.Effector.ApplyDamageReduction(damageReduction, effectDuration).Forget();
        return UniTask.CompletedTask;
    }
}
