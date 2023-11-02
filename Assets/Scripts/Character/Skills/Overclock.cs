using Cysharp.Threading.Tasks;
using UnityEngine;

public class Overclock : Skill
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float range;

    [SerializeField]
    private float recoveryTime;

    [SerializeField]
    private int shotCount;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override bool CanUse => finder.Character != null && IsWithinRange;
    private bool IsWithinRange =>
        (transform.position - finder.Character.transform.position).sqrMagnitude <= range;

    protected override async UniTask ApplyEffect()
    {
        var targetHealth = finder.Character.Health;
        for (int i = 0; i < shotCount && !targetHealth.IsDead; i++)
        {
            targetHealth.GetDamaged(damage);
            await UniTask.WaitForSeconds(recoveryTime, ignoreTimeScale: true);
        }
    }
}
