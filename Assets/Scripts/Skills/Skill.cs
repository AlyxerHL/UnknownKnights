using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    private int cooldown;

    [SerializeField]
    protected TargetTagFinder tagFinder;

    public abstract void Use();
}
