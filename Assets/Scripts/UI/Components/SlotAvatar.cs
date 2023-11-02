using System;
using UnityEngine;

public class SlotAvatar : MonoBehaviour
{
    [field: SerializeField]
    public Character CharacterPrefab { get; private set; }

    public event Action<Character> Selected;

    public void Select()
    {
        Selected?.Invoke(CharacterPrefab);
    }
}
