using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BattleReferee : MonoBehaviour
{
    [SerializeField]
    private int timeLimitInSeconds;

    private int _time;

    public event Action<int> OnTimeChanged;

    public int Time
    {
        get => _time;
        set
        {
            _time = value;
            OnTimeChanged?.Invoke(_time);
        }
    }

    private void Start()
    {
        Time = timeLimitInSeconds;

        DOTween
            .To(() => Time, (x) => Time = x, 0, timeLimitInSeconds)
            .SetEase(Ease.Linear)
            .OnComplete(() => Debug.Log("Time's up!"));
    }

    public static void MakeDecision()
    {
        if (CharacterTag.ActiveTags.Count == 0)
        {
            Debug.Log("Is this even possible?");
            return;
        }

        var firstTag = CharacterTag.ActiveTags.First().tag;
        var isFinished = CharacterTag.ActiveTags.All((tag) => tag.CompareTag(firstTag));

        if (isFinished)
        {
            Debug.Log(firstTag + " Win!");
        }
    }
}
