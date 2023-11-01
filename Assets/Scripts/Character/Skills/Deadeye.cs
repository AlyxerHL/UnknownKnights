using Cysharp.Threading.Tasks;
using UnityEngine;

public class Deadeye : Skill
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float aimingTime;

    [SerializeField]
    private float recoveryTime;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override bool CanUse => finder.Character != null;

    protected override async UniTask ApplyEffect()
    {
        await UniTask.WaitForSeconds(aimingTime, ignoreTimeScale: true);
        finder.Character.Health.GetDamaged(damage);
        await UniTask.WaitForSeconds(recoveryTime, ignoreTimeScale: true);
    }
}
