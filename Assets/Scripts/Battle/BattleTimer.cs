using System;
using DG.Tweening;
using UnityEngine;

public class BattleTimer : MonoBehaviour
{
    [SerializeField]
    private int timeLimitInSeconds;

    private int timeLeftInSeconds;

    public event Action<int> OnTimeChanged;

    private void Start()
    {
        timeLeftInSeconds = timeLimitInSeconds;
        OnTimeChanged?.Invoke(timeLeftInSeconds);

        DOTween
            .To(() => timeLeftInSeconds, (x) => timeLeftInSeconds = x, 0, timeLimitInSeconds)
            .SetEase(Ease.Linear)
            .OnUpdate(() => OnTimeChanged?.Invoke(timeLeftInSeconds))
            .OnComplete(() => Debug.Log("Time's up!"));
    }
}
