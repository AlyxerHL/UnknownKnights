using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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

    [SerializeField]
    private float healthFillDuration;

    private float greenTeamMaxHealth;
    private float redTeamMaxHealth;
    private Tweener greenTeamHealthTweener;
    private Tweener redTeamHealthTweener;

    private void Awake()
    {
        timeLeft.OnValueChanged += (time) =>
        {
            var minutes = time / 60;
            var seconds = time % 60;
            timer.text = $"{minutes:00}:{seconds:00}";
        };

        greenTeamHealthTweener = greenTeamHealth
            .DOFillAmount(1f, healthFillDuration)
            .SetAutoKill(false);

        greenTeamTotalHealth.OnValueChanged += (totalHealth) =>
        {
            var fillAmount = totalHealth / greenTeamMaxHealth;
            greenTeamHealthTweener.ChangeEndValue(fillAmount, true).Restart();
        };

        redTeamHealthTweener = redTeamHealth
            .DOFillAmount(1f, healthFillDuration)
            .SetAutoKill(false);

        redTeamTotalHealth.OnValueChanged += (totalHealth) =>
        {
            var fillAmount = totalHealth / redTeamMaxHealth;
            redTeamHealthTweener.ChangeEndValue(fillAmount, true).Restart();
        };

        referee.OnTimeChanged += (time) => timeLeft.Value = time;

        characterSpawner.OnCharacterSpawned += (character) =>
        {
            if (character.CompareTag(CharacterSpawner.GreenTeamTag))
            {
                greenTeamMaxHealth += character.Health.MaxHealth;
                greenTeamTotalHealth.Value = greenTeamMaxHealth;
                character.Health.OnHealthChanged += (changeAmount) =>
                    greenTeamTotalHealth.Value += changeAmount;
            }
            else if (character.CompareTag(CharacterSpawner.RedTeamTag))
            {
                redTeamMaxHealth += character.Health.MaxHealth;
                redTeamTotalHealth.Value = redTeamMaxHealth;
                character.Health.OnHealthChanged += (changeAmount) =>
                    redTeamTotalHealth.Value += changeAmount;
            }
        };
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
