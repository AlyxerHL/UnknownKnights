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
        tagFinder.TargetTag != null
        && (transform.position - tagFinder.TargetTag.transform.position).sqrMagnitude <= range;

    protected override async UniTask Fire()
    {
        for (int i = 0; i < burstCount && CanFire; i++)
        {
            tagFinder.TargetTag.Health.GetDamaged(damage);
            if (i < burstCount - 1)
            {
                await UniTask.Delay(burstInterval);
            }
        }
    }
}
