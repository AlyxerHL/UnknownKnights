using Cysharp.Threading.Tasks;
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

    private RectTransform rectTransform;

    public IObjectPool<DamageView> PoolToReturn { get; set; }

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    public void Show(Transform target, float damage)
    {
        text.text = damage.ToString();

        var position =
            (Vector2)Camera.main.WorldToScreenPoint(target.position)
            + Random.insideUnitCircle * randomness;
        rectTransform.anchoredPosition = position;

        rectTransform
            .DOAnchorPos(position + offset, duration)
            .SetEase(Ease.OutExpo)
            .SetUpdate(true)
            .OnComplete(() => PoolToReturn.Release(this));
    }
}
