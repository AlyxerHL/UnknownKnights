using UnityEngine;

public static class Single
{
    public static bool Approximately(this float a, float b) => Mathf.Approximately(a, b);
}
