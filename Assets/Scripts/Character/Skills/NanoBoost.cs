using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NanoBoost : Skill
{
    private const int EffectDuration = 4000;

    [SerializeField]
    private NearestFriendlyCharacterFinder finder;

    protected override bool CanUse => true;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        finder.Tag.Health.DamageRate = 0.5f;
        finder.Tag.Weapon.DamageRate = 2f;
        await UniTask.Delay(EffectDuration, cancellationToken: cancellationToken);
        finder.Tag.Health.DamageRate = 1f;
        finder.Tag.Weapon.DamageRate = 1f;
    }
}
