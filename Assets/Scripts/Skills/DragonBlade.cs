using UnityEngine;

public class DragonBlade : Skill
{
    [Header(nameof(DragonBlade))]
    [SerializeField]
    private float damage;

    public override void Use()
    {
        tagFinder.TargetTag.Health.GetDamaged(damage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Use();
        }
    }
}
