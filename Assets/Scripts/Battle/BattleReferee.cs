using System;
using System.Linq;
using Cysharp.Threading.Tasks;
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

    private void Awake()
    {
        var tweener = DOTween
            .To(() => time.Value, (x) => time.Value = x, 0f, timeLimit)
            .SetEase(Ease.Linear)
            .OnComplete(() => Debug.Log("Time's up!"));

        time.Value = timeLimit;
        BattleTime.TimeScaleChanged += (timeScale) => tweener.timeScale = timeScale;
    }

    public static void MakeDecision()
    {
        var firstTag = Character.Active.First().tag;
        var isFinished = Character.Active.All((character) => character.CompareTag(firstTag));

        if (isFinished)
        {
            PagesRouter
                .GoTo("ResultPage")
                .ContinueWith(
                    (page) =>
                    {
                        var resultPage = page.GetComponent<ResultPage>();
                        resultPage.Initialize(firstTag == CharacterSpawner.GreenTeamTag);
                    }
                );
        }
    }
}
