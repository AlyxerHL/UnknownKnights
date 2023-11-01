using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Vector2 offset;

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

        var screenPosition = Camera.main.WorldToScreenPoint(target.position);
        rectTransform.anchoredPosition = (Vector2)screenPosition + offset;
    }

    public void Initialize(Health health)
    {
        target = health.transform;
        image.fillAmount = health.CurrentHealth / health.MaxHealth;
        health.Changed += (_) => image.fillAmount = health.CurrentHealth / health.MaxHealth;
        health.Dead += () => gameObject.SetActive(false);
    }
}
