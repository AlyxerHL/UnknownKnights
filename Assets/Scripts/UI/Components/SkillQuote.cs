using TMPro;
using UnityEngine;

public class SkillQuote : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

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

        var canvasPosition = Camera.main.WorldToCanvasPoint(target.position);
        rectTransform.anchoredPosition = canvasPosition + offset;
    }

    public void Initialize(Skill skill)
    {
        target = skill.transform;
        text.text = skill.Quote;
    }
}
