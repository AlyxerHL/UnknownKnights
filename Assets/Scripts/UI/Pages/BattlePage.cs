using System.Linq;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattlePage : Page
{
    private readonly State<int> timeLeft = new();
    private readonly State<float> greenTeamTotalHealth = new();
    private readonly State<float> redTeamTotalHealth = new();

    [SerializeField]
    private TextMeshProUGUI timer;

    [SerializeField]
    private Image greenTeamHealth;

    [SerializeField]
    private Image redTeamHealth;

    [SerializeField]
    private CharacterSpawner characterSpawner;

    [SerializeField]
    private BattleReferee referee;

    private float greenTeamMaxHealth;
    private float redTeamMaxHealth;

    private void Awake()
    {
        timeLeft.OnValueChanged += (time) =>
        {
            var minutes = time / 60;
            var seconds = time % 60;
            timer.text = $"{minutes:00}:{seconds:00}";
        };

        greenTeamTotalHealth.OnValueChanged += (totalHealth) =>
        {
            var fillAmount = totalHealth / greenTeamMaxHealth;
            greenTeamHealth.fillAmount = fillAmount;
        };

        redTeamTotalHealth.OnValueChanged += (totalHealth) =>
        {
            var fillAmount = totalHealth / redTeamMaxHealth;
            redTeamHealth.fillAmount = fillAmount;
        };
    }

    private void Start()
    {
        referee.OnTimeChanged += (time) => timeLeft.Value = time;

        greenTeamMaxHealth = characterSpawner.GreenTeamCharacters.Sum(
            (character) => character.Health.MaxHealth
        );

        redTeamMaxHealth = characterSpawner.RedTeamCharacters.Sum(
            (character) => character.Health.MaxHealth
        );

        greenTeamTotalHealth.Value = greenTeamMaxHealth;
        redTeamTotalHealth.Value = redTeamMaxHealth;

        characterSpawner.GreenTeamCharacters.ForEach(
            (character) =>
                character.Health.OnHealthChanged += (changeAmount) =>
                    greenTeamTotalHealth.Value += changeAmount
        );

        characterSpawner.RedTeamCharacters.ForEach(
            (character) =>
                character.Health.OnHealthChanged += (changeAmount) =>
                    redTeamTotalHealth.Value += changeAmount
        );
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
