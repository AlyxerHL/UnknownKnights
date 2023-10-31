using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterTag : MonoBehaviour
{
    public static readonly List<CharacterTag> ActiveTags = new();

    [field: SerializeField]
    public Health Health { get; private set; }

    [field: SerializeField]
    public Effector Effector { get; private set; }

    private void OnEnable()
    {
        ActiveTags.Add(this);
    }

    private void OnDisable()
    {
        ActiveTags.Remove(this);
    }
}
