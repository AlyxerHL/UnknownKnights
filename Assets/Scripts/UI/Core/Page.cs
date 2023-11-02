using UnityEngine;
using Cysharp.Threading.Tasks;

public class Page : MonoBehaviour
{
    [SerializeField]
    private Transition transition;

    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = transform as RectTransform;
    }

    public UniTask Show()
    {
        return transition.Show();
    }

    public UniTask Hide()
    {
        return transition.Show();
    }
}
