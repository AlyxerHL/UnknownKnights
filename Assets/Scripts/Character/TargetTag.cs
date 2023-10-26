using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class TargetTag : MonoBehaviour
{
    public static readonly List<TargetTag> ActiveTargetTags = new();

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        ActiveTargetTags.Add(this);
    }

    private void OnDisable()
    {
        ActiveTargetTags.Remove(this);
    }
}
