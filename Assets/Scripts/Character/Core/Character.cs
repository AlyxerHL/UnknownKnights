using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static readonly List<Character> Active = new();

    [field: SerializeField]
    public Health Health { get; private set; }

    [field: SerializeField]
    public Effector Effector { get; private set; }

    private void OnEnable()
    {
        Active.Add(this);
    }

    private void OnDisable()
    {
        Active.Remove(this);
    }
}
