using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    public abstract UniTask Show();
    public abstract UniTask Hide();
}
