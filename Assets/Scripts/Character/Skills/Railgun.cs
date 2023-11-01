using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Railgun : Skill
{
    [SerializeField]
    private float damage = 135f;

    [SerializeField]
    private float range = 40f;

    [SerializeField]
    private int recoveryTime = 650;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    private bool IsWithinRange =>
        (transform.position - finder.Tag.transform.position).sqrMagnitude <= range;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        if (finder.Tag == null || !IsWithinRange)
        {
            return;
        }

        finder.Tag.Health.GetDamaged(damage);
        await UniTask.Delay(recoveryTime, cancellationToken: cancellationToken);
    }
}
