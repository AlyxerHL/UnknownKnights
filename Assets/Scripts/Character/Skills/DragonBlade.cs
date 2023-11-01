using Cysharp.Threading.Tasks;
using UnityEngine;

public class DragonBlade : Skill
{
    [SerializeField]
    private float damage = 110f;

    [SerializeField]
    private float range = 1f;

    [SerializeField]
    private float recoveryTime = 0.9f;

    [SerializeField]
    private int swingCount = 2;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override bool CanUse => finder.Character != null && IsWithinRange;
    private bool IsWithinRange =>
        (transform.position - finder.Character.transform.position).sqrMagnitude <= range;

    protected override async UniTask ApplyEffect()
    {
        for (int i = 0; i < swingCount; i++)
        {
            finder.Character.Health.GetDamaged(damage);
            await UniTask.WaitForSeconds(recoveryTime, ignoreTimeScale: true);
        }
    }
}
