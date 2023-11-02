using UnityEngine;

public static class TimeSystem
{
    public static float BaseTimeScale { get; set; } = 1f;

    public static void ResetTimeScale()
    {
        Time.timeScale = BaseTimeScale;
    }

    public static void StopTimeScale()
    {
        Time.timeScale = 0f;
    }
}
