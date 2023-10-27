using Cysharp.Threading.Tasks;
using UnityEngine;

public class Shuriken : Weapon
{
    [Header(nameof(Shuriken))]
    [SerializeField]
    private float damage;

    [SerializeField]
    private float range;

    [SerializeField]
    private int burstCount;

    [SerializeField]
    private int burstInterval;

    protected override bool CanFire =>
        targetTagFinder.TargetTag != null
        && (transform.position - targetTagFinder.TargetTag.transform.position).sqrMagnitude
            <= range;

    protected override async UniTask Fire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            if (i != 0)
            {
                await UniTask.Delay(burstInterval);
            }

            if (targetTagFinder.TargetTag == null)
            {
                break;
            }

            targetTagFinder.TargetTag.Health.TakeDamage(damage);
        }
    }
}
