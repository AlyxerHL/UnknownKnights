using UnityEngine;

public static class SingleExtension
{
    public static bool Approximately(this float a, float b) => Mathf.Approximately(a, b);
}
