using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Vector2 offset;

    [Header("Effects")]
    [SerializeField]
    private GameObject stunEffect;

    [SerializeField]
    private GameObject damageBuffEffect;

    [SerializeField]
    private GameObject damageReductionEffect;

    private Transform target;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        var canvasPosition = Camera.main.WorldToCanvasPoint(target.position);
        rectTransform.anchoredPosition = canvasPosition + offset;
    }

    public void Initialize(Health health, Effector effector)
    {
        target = health.transform;
        image.fillAmount = health.CurrentHealth / health.MaxHealth;
        health.Changed += (_) => image.fillAmount = health.CurrentHealth / health.MaxHealth;
        health.Dead += () => gameObject.SetActive(false);

        effector.StunRefreshed += (isStunned) => stunEffect.SetActive(isStunned);
        effector.DamageBuffRefreshed += (isBuffed) => damageBuffEffect.SetActive(isBuffed > 1f);
        effector.DamageReductionRefreshed += (isReduced) =>
            damageReductionEffect.SetActive(isReduced < 1f);
    }
}
