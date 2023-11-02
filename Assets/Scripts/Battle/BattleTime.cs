using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class BattleTime
{
    private static float timeScale = 1f;
    private static float baseTimeScale = 1f;

    public static bool IsPaused => timeScale == 0f;

    public static float BaseTimeScale
    {
        get => baseTimeScale;
        set
        {
            baseTimeScale = value;
            if (!IsPaused)
            {
                timeScale = value;
                TimeScaleChanged?.Invoke(timeScale);
            }
        }
    }

    public static event Action<float> TimeScaleChanged;

    public static void PauseTimeScale()
    {
        timeScale = 0f;
        TimeScaleChanged?.Invoke(timeScale);
    }

    public static void ResumeTimeScale()
    {
        timeScale = BaseTimeScale;
        TimeScaleChanged?.Invoke(timeScale);
    }

    public static async UniTask WaitForSeconds(
        float seconds,
        CancellationToken cancellationToken = default
    )
    {
        float elapsedTime = 0f;
        while (elapsedTime < seconds && !cancellationToken.IsCancellationRequested)
        {
            await UniTask.Yield(cancellationToken);
            elapsedTime += Time.deltaTime * timeScale;
        }
    }
}
