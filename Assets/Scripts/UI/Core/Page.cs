using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class Page : MonoBehaviour
{
    public abstract UniTask Show();
    public abstract UniTask Hide();
}
