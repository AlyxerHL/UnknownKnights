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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeRemaining -= 1;
            OnTimeChanged?.Invoke(timeRemaining);
            Debug.Log(timeRemaining);
        }
    }
}
