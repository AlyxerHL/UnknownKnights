using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NanoBoost : Skill
{
    [SerializeField]
    private int effectDuration = 4000;

    [SerializeField]
    private NearestFriendlyCharacterFinder finder;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        if (finder.Tag == null)
        {
            return;
        }

        var effector = finder.Tag.Effector;
        var damageBuffID = effector.SetDamageBuff(2f);
        var damageReductionID = effector.SetDamageReduction(0.5f);
        await UniTask.Delay(effectDuration, cancellationToken: cancellationToken);

        if (effector != null)
        {
            effector.ClearDamageBuff(damageBuffID);
            effector.ClearDamageReduction(damageReductionID);
        }
    }
}
