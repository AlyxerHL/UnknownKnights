using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattlePage : Page
{
    private RectTransform rectTransform;

    public override RectTransform RectTransform => rectTransform;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
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
