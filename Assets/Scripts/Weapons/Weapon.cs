using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private readonly CancellationTokenSource autoFire = new();

    [SerializeField]
    private int cooldown;

    [SerializeField]
    protected TargetTagFinder tagFinder;

    protected abstract bool CanFire { get; }

    private void OnEnable()
    {
        FireAutomatically(autoFire.Token).Forget();
    }

    private void OnDisable()
    {
        autoFire.Cancel();
    }

    private async UniTaskVoid FireAutomatically(CancellationToken cancellationToken)
    {
        while (true)
        {
            await UniTask.Delay(cooldown, cancellationToken: cancellationToken);
            await UniTask.WaitUntil(() => CanFire, cancellationToken: cancellationToken);
            await Fire(cancellationToken);
        }
    }

    protected abstract UniTask Fire(CancellationToken cancellationToken);
}
