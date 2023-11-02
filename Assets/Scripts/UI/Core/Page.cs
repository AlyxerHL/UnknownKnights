using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class Page : MonoBehaviour
{
    public abstract RectTransform RectTransform { get; }

    public abstract UniTask Show();
    public abstract UniTask Hide();
}
