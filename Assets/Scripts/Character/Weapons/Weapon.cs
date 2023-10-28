using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private CancellationTokenSource cancellation;

    private void OnEnable()
    {
        StartFiring();
    }

    private void OnDisable()
    {
        StopFiring();
    }

    public void StartFiring()
    {
        cancellation = new();
        StartFiring().Forget();

        async UniTaskVoid StartFiring()
        {
            while (!cancellation.Token.IsCancellationRequested)
            {
                await Fire(cancellation.Token);
            }
        }
    }

    public void StopFiring()
    {
        cancellation.Cancel();
    }

    protected abstract UniTask Fire(CancellationToken cancellationToken);
}
