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

    private Tweener tweener;

    public void Initialize(Sprite sprite, Skill skill, Health health)
    {
        avatar.sprite = sprite;
        skill.CooldownBegan += OnCooldownBegan;
        button.onClick.AddListener(() => skill.Use().Forget());

        health.Changed += (_) => healthBar.fillAmount = health.CurrentHealth / health.MaxHealth;
        health.Dead += OnDead;
    }

    private void OnCooldownBegan(float time)
    {
        tweener?.Kill();
        button.interactable = false;
        var cooldownTime = time;

        tweener = DOTween
            .To(() => cooldownTime, (x) => cooldownTime = x, 0f, cooldownTime)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                cooldownGauge.fillAmount = cooldownTime / time;
                cooldown.text = Mathf.CeilToInt(cooldownTime).ToString();
            })
            .OnComplete(() =>
            {
                button.interactable = true;
                cooldown.text = string.Empty;
            });
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
