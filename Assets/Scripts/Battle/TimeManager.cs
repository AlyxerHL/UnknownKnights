using System;
using DG.Tweening;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private int timeLimit;

    private int timeRemaining;

    public static event Action<int> OnTimeChanged;

    private void Start()
    {
        timeRemaining = timeLimit;
        OnTimeChanged?.Invoke(timeRemaining);

        DOTween
            .To(() => timeRemaining, (x) => timeRemaining = x, 0, timeLimit)
            .SetEase(Ease.Linear)
            .OnUpdate(() => OnTimeChanged?.Invoke(timeRemaining))
            .OnComplete(() => Debug.Log("Time's up!"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
    }
}
