using UnityEngine;

public class DamageSkill : Skill
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float range;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    protected override bool CanUse => finder.Character != null && IsWithinRange;
    private bool IsWithinRange =>
        (transform.position - finder.Character.transform.position).sqrMagnitude <= range;

    protected override void ApplyEffect()
    {
        finder.Character.Health.GetDamaged(damage);
    }
}
