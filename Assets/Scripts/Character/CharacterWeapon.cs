using UnityEngine;

[RequireComponent(typeof(CharacterTargeter))]
public abstract class CharacterWeapon : MonoBehaviour
{
    [SerializeField]
    protected float damage = 30f;

    [SerializeField]
    protected float range = 1f;

    [SerializeField]
    protected float cooldown = 0.5f;

    protected CharacterTargeter targeter;

    private void Awake()
    {
        targeter = GetComponent<CharacterTargeter>();
    }

    public abstract void Use();
}
