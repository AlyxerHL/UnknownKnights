using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    private int cooldown;

    public abstract void Use();
}
