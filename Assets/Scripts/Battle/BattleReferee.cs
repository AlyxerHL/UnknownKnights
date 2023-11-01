using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BattleReferee : MonoBehaviour
{
    [SerializeField]
    private float timeLimit;

    private float _time;

    public event Action<float> OnTimeChanged;

    public float Time
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
        Time = timeLimit;
        DOTween
            .To(() => Time, (x) => Time = x, 0f, timeLimit)
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
