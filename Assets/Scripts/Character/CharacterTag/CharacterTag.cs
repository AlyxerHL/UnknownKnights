using System.Collections.Generic;
using UnityEngine;

public class CharacterTag : MonoBehaviour
{
    public static readonly List<CharacterTag> ActiveTags = new();

    [field: SerializeField]
    public Health Health { get; private set; }

    [field: SerializeField]
    public Movement Movement { get; private set; }

    [field: SerializeField]
    public Weapon Weapon { get; private set; }

    [field: SerializeField]
    public Skill Skill { get; private set; }

    private void OnEnable()
    {
        ActiveTags.Add(this);
    }

    private void OnDisable()
    {
        ActiveTags.Remove(this);
    }
}
