using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class DamageView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private float duration;

    [SerializeField]
    private Vector2 offset;

    [SerializeField]
    private Vector2 randomness;

    [SerializeField]
    private Color damageColor;

    [SerializeField]
    private Color healColor;

    private RectTransform rectTransform;

    public IObjectPool<DamageView> PoolToReturn { get; set; }

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    public void Show(Transform target, float amount)
    {
        text.text = Mathf.Abs(amount).ToString();
        text.color = amount < 0 ? damageColor : healColor;

        var position =
            Camera.main.WorldToCanvasPoint(target.position) + Random.insideUnitCircle * randomness;
        rectTransform.anchoredPosition = position;

        rectTransform
            .DOAnchorPos(position + offset, duration)
            .SetEase(Ease.OutExpo)
            .OnComplete(() => PoolToReturn.Release(this));
    }
}
