using System.Collections.Generic;
using UnityEngine;

public class TargetTag : MonoBehaviour
{
    public static readonly List<TargetTag> ActiveTargetTags = new();

    [field: SerializeField]
    public bool IsFriendly { get; private set; }

    [field: SerializeField]
    public Health Health { get; private set; }

    private void OnEnable()
    {
        ActiveTargetTags.Add(this);
    }

    private void OnDisable()
    {
        ActiveTargetTags.Remove(this);
    }
}
