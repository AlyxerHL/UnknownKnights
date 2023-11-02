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

        tweener.timeScale = BattleTime.TimeScale;
        BattleTime.TimeScaleChanged += (timeScale) => tweener.timeScale = timeScale;
        time.Value = timeLimit;
    }

    public static void MakeDecision()
    {
        var firstTag = Character.Active.First().tag;
        var isFinished = Character.Active.All((character) => character.CompareTag(firstTag));

        if (isFinished)
        {
            ShowResult(firstTag).Forget();
        }
    }

    private static async UniTaskVoid ShowResult(string firstTag)
    {
        BattleTime.PauseTimeScale();
        await UniTask.WaitForSeconds(1f);
        var page = await PagesRouter.GoTo("ResultPage");
        var resultPage = page.GetComponent<ResultPage>();
        resultPage.Initialize(firstTag == CharacterSpawner.GreenTeamTag);
    }
}
