using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Deadeye : Skill
{
    [SerializeField]
    private float damage = 250f;

    [SerializeField]
    private float aimingTime = 3f;

    [SerializeField]
    private float recoveryTime = 0.3f;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        await UniTask.WaitForSeconds(aimingTime, cancellationToken: cancellationToken);
        if (finder.Character != null)
        {
            finder.Character.Health.GetDamaged(damage);
        }
        await UniTask.WaitForSeconds(recoveryTime, cancellationToken: cancellationToken);
    }
}
