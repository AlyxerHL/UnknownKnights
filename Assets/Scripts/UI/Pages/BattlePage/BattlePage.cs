using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattlePage : Page
{
    [SerializeField]
    private BattlePageHealthBar healthBar;

    [SerializeField]
    private BattlePageScoreboard scoreboard;

    [SerializeField]
    private CharacterSpawner characterSpawner;

    [SerializeField]
    private BattleReferee battleReferee;

    private void Awake()
    {
        healthBar.Initialize(characterSpawner);
        scoreboard.Initialize(characterSpawner, battleReferee);
    }

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
