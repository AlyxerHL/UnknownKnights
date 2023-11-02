using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillAvatar : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Image avatar;

    [SerializeField]
    private TextMeshProUGUI cooldown;

    [SerializeField]
    private Image cooldownGauge;

    [SerializeField]
    private Image healthBar;

    private float cooldownLeft;
    private Tweener tweener;

    public void Initialize(Sprite sprite, Skill skill, Health health)
    {
        avatar.sprite = sprite;
        skill.CooldownBegan += OnCooldownBegan;
        button.onClick.AddListener(() => skill.Use().Forget());
        health.Changed += (_) => healthBar.fillAmount = health.CurrentHealth / health.MaxHealth;
        health.Dead += OnDead;

        tweener = DOTween
            .To(() => cooldownLeft, (x) => cooldownLeft = x, 0f, 0f)
            .SetEase(Ease.Linear)
            .SetAutoKill(false);
        BattleTime.TimeScaleChanged += (timeScale) => tweener.timeScale = timeScale;
    }

    private void OnCooldownBegan(float cooldownTime)
    {
        button.interactable = false;
        cooldownLeft = cooldownTime;

        tweener
            .ChangeStartValue(cooldownLeft, cooldownTime)
            .OnUpdate(() =>
            {
                cooldownGauge.fillAmount = cooldownLeft / cooldownTime;
                cooldown.text = Mathf.CeilToInt(cooldownLeft).ToString();
            })
            .OnComplete(() =>
            {
                button.interactable = true;
                cooldown.text = string.Empty;
            })
            .Restart();
    }

    private void OnDead()
    {
        tweener?.Kill();
        button.interactable = false;
        avatar.color = Color.red;
        cooldown.gameObject.SetActive(false);
        cooldownGauge.gameObject.SetActive(false);
    }
}
