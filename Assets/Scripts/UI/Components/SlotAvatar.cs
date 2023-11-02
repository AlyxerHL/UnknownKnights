using System;
using UnityEngine;

public class SlotAvatar : MonoBehaviour
{
    [SerializeField]
    private Character characterPrefab;

    public event Action<Character> Selected;

    public void Select()
    {
        Selected?.Invoke(characterPrefab);
    }
}
 