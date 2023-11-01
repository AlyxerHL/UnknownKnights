using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattlePageScoreboard : MonoBehaviour
{
    [SerializeField]
    CharacterSpawner characterSpawner;

    [SerializeField]
    BattleReferee battleReferee;

    [SerializeField]
    private TextMeshProUGUI timer;

    [SerializeField]
    private Image greenTeamGauge;

    [SerializeField]
    private Image redTeamGauge;

    [SerializeField]
    private float gaugeSpeed;

    private readonly State<float> greenTeamTotalHealth = new();
    private readonly State<float> redTeamTotalHealth = new();
    private float greenTeamMaxHealth;
    private float redTeamMaxHealth;

    private Tweener greenTeamGaugeTweener;
    private Tweener redTeamGaugeTweener;

    private void Awake()
    {
        greenTeamGaugeTweener = greenTeamGauge.DOFillAmount(1f, gaugeSpeed).SetAutoKill(false);
        greenTeamTotalHealth.Updated += (totalHealth) =>
        {
            var fillAmount = totalHealth / greenTeamMaxHealth;
            greenTeamGaugeTweener.ChangeEndValue(fillAmount, true).Restart();
        };

        redTeamGaugeTweener = redTeamGauge.DOFillAmount(1f, gaugeSpeed).SetAutoKill(false);
        redTeamTotalHealth.Updated += (totalHealth) =>
        {
            var fillAmount = totalHealth / redTeamMaxHealth;
            redTeamGaugeTweener.ChangeEndValue(fillAmount, true).Restart();
        };

        characterSpawner.CharacterSpawned += (character) =>
        {
            if (character.CompareTag(CharacterSpawner.GreenTeamTag))
            {
                greenTeamMaxHealth += character.Health.MaxHealth;
                greenTeamTotalHealth.Value = greenTeamMaxHealth;
                character.Health.Changed += (changeAmount) =>
                    greenTeamTotalHealth.Value += changeAmount;
            }
            else if (character.CompareTag(CharacterSpawner.RedTeamTag))
            {
                redTeamMaxHealth += character.Health.MaxHealth;
                redTeamTotalHealth.Value = redTeamMaxHealth;
                character.Health.Changed += (changeAmount) =>
                    redTeamTotalHealth.Value += changeAmount;
            }
        };

        battleReferee.TimePassed += (time) =>
        {
            var ceilTime = Mathf.CeilToInt(time);
            var minutes = ceilTime / 60;
            var seconds = ceilTime % 60;
            timer.text = $"{minutes:00}:{seconds:00}";
        };
    }
}
