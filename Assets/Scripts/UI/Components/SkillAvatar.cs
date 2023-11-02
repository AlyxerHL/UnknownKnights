using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillAvatar : MonoBehaviour
{
    [SerializeField]
    private Button avatar;

    [SerializeField]
    private TextMeshProUGUI cooldown;

    [SerializeField]
    private Image cooldownGauge;

    [SerializeField]
    private Image healthBar;

    public void Initialize(Sprite sprite, Skill skill, Health health)
    {
        avatar.image.sprite = sprite;
        skill.CooldownBegan += OnCooldownBegan;
        health.Changed += (_) => healthBar.fillAmount = health.CurrentHealth / health.MaxHealth;
        avatar.onClick.AddListener(() => skill.Use().Forget());
    }

    private void OnCooldownBegan(float time)
    {
        avatar.interactable = false;
        var cooldownTime = time;

        DOTween
            .To(() => cooldownTime, (x) => cooldownTime = x, 0f, cooldownTime)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                cooldownGauge.fillAmount = cooldownTime / time;
                cooldown.text = Mathf.CeilToInt(cooldownTime).ToString();
            })
            .OnComplete(() =>
            {
                avatar.interactable = true;
                cooldown.text = string.Empty;
            });
    }
}
