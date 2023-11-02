using UnityEngine;

public static class TimeSystem
{
    private static float baseTimeScale = 1f;
    private static bool isStopped = false;

    public static float BaseTimeScale
    {
        get => baseTimeScale;
        set
        {
            baseTimeScale = value;
            if (!isStopped)
            {
                Time.timeScale = value;
            }
        }
    }

    public static void ResetTimeScale()
    {
        isStopped = false;
        Time.timeScale = BaseTimeScale;
    }

    public static void StopTimeScale()
    {
        isStopped = true;
        Time.timeScale = 0f;
    }
}
