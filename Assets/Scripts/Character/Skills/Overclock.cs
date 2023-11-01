using Cysharp.Threading.Tasks;
using UnityEngine;

public class Overclock : Skill
{
    [SerializeField]
    private float damage = 135f;

    [SerializeField]
    private float range = 40f;

    [SerializeField]
    private float recoveryTime = 0.65f;

    [SerializeField]
    private int shotCount = 4;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override bool CanUse => finder.Character != null && IsWithinRange;
    private bool IsWithinRange =>
        (transform.position - finder.Character.transform.position).sqrMagnitude <= range;

    protected override async UniTask ApplyEffect()
    {
        for (int i = 0; i < shotCount; i++)
        {
            finder.Character.Health.GetDamaged(damage);
            await UniTask.WaitForSeconds(recoveryTime, ignoreTimeScale: true);
        }
    }
}
