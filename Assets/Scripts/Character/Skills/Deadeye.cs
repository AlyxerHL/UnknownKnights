using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Deadeye : Skill
{
    [SerializeField]
    private float damage = 250f;

    [SerializeField]
    private int aimingTime = 3000;

    [SerializeField]
    private int recoveryTime = 300;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        await UniTask.Delay(aimingTime, cancellationToken: cancellationToken);
        if (finder.Tag != null)
        {
            finder.Tag.Health.GetDamaged(damage);
        }
        await UniTask.Delay(recoveryTime, cancellationToken: cancellationToken);
    }
}
