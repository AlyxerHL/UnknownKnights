using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DragonBlade : Skill
{
    [SerializeField]
    private float damage = 70f;

    [SerializeField]
    private float range = 1f;

    [SerializeField]
    private float recoveryTime = 0.9f;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    private bool IsWithinRange =>
        (transform.position - finder.Tag.transform.position).sqrMagnitude <= range;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        if (finder.Tag != null && IsWithinRange)
        {
            finder.Tag.Health.GetDamaged(damage);
        }
        await UniTask.WaitForSeconds(recoveryTime, cancellationToken: cancellationToken);
    }
}
