using System.Collections.Generic;
using UnityEngine;

public class TargetableCharacter : MonoBehaviour
{
    public static readonly List<TargetableCharacter> ActiveEntities = new();

    private void OnEnable()
    {
        ActiveEntities.Add(this);
    }

    private void OnDisable()
    {
        ActiveEntities.Remove(this);
    }
}
