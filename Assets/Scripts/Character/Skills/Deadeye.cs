using Cysharp.Threading.Tasks;
using UnityEngine;

public class Deadeye : Skill
{
    [SerializeField]
    private float damage = 780f;

    [SerializeField]
    private float aimingTime = 3f;

    [SerializeField]
    private float recoveryTime = 0.3f;

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
