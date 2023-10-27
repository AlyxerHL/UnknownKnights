using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private readonly CancellationTokenSource cancellation = new();

    [SerializeField]
    protected TargetTagFinder tagFinder;

    protected abstract bool CanFire { get; }

    private void OnEnable()
    {
        FireAutomatically(cancellation.Token).Forget();
    }

    private void OnDisable()
    {
        cancellation.Cancel();
    }

    protected abstract UniTask Fire(CancellationToken cancellationToken);

    private async UniTaskVoid FireAutomatically(CancellationToken cancellationToken)
    {
        while (true)
        {
            await UniTask.WaitUntil(() => CanFire, cancellationToken: cancellationToken);
            await Fire(cancellationToken);
        }
    }
}
