using Cysharp.Threading.Tasks;
using UnityEngine;

public class DragonBlade : Skill
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float range;

    [SerializeField]
    private float recoveryTime;

    [SerializeField]
    private int swingCount;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override bool CanUse => finder.Character != null && IsWithinRange;
    private bool IsWithinRange =>
        (transform.position - finder.Character.transform.position).sqrMagnitude <= range;

    protected override async UniTask ApplyEffect()
    {
        var target = finder.Character;
        for (int i = 0; i < swingCount && target != null; i++)
        {
            target.Health.GetDamaged(damage);
            await UniTask.WaitForSeconds(recoveryTime, ignoreTimeScale: true);
        }
    }
}
