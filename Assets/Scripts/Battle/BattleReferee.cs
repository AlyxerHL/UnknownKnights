using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BattleReferee : MonoBehaviour
{
    [SerializeField]
    private float timeLimit;

    private readonly State<float> time = new();

    public event Action<float> TimePassed
    {
        add => time.Updated += value;
        remove => time.Updated -= value;
    }

    private void Start()
    {
        time.Value = timeLimit;
        DOTween
            .To(() => time.Value, (x) => time.Value = x, 0f, timeLimit)
            .SetEase(Ease.Linear)
            .OnComplete(() => Debug.Log("Time's up!"));
    }

    public static void MakeDecision()
    {
        if (Character.Active.Count == 0)
        {
            Debug.Log("Is this even possible?");
            return;
        }

        var firstTag = Character.Active.First().tag;
        var isFinished = Character.Active.All((tag) => tag.CompareTag(firstTag));

        if (isFinished)
        {
            Debug.Log(firstTag + " Win!");
        }
    }
}
