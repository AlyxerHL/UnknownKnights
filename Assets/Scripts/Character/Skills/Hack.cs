using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Hack : Skill
{
    private const float Range = 15f;
    private const int EffectDuration = 1500;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override bool CanUse =>
        finder.Tag != null
        && (transform.position - finder.Tag.transform.position).sqrMagnitude <= Range;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        finder.Tag.Movement.enabled = false;
        finder.Tag.Weapon.enabled = false;
        finder.Tag.Skill.enabled = false;

        await UniTask.Delay(EffectDuration, cancellationToken: cancellationToken);
        finder.Tag.Movement.enabled = true;
        finder.Tag.Weapon.enabled = true;
        finder.Tag.Skill.enabled = true;
    }
}
