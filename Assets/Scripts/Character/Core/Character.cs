using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static readonly List<Character> Active = new();

    [field: SerializeField]
    public Health Health { get; private set; }

    [field: SerializeField]
    public Movement Movement { get; private set; }

    [field: SerializeField]
    public Weapon Weapon { get; private set; }

    [field: SerializeField]
    public Skill Skill { get; private set; }

    public Effector Effector { get; private set; }

    private void Awake()
    {
        Effector = new Effector(this);
    }

    private void OnEnable()
    {
        Active.Add(this);
    }

    private void OnDisable()
    {
        Active.Remove(this);
    }
}
