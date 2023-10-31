using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattlePage : Page
{
    [SerializeField]
    private Scoreboard scoreboard;

    public override UniTask Hide()
    {
        gameObject.SetActive(false);
        return UniTask.CompletedTask;
    }

    public override UniTask Show()
    {
        gameObject.SetActive(true);
        return UniTask.CompletedTask;
    }
}
