using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected TargetTagFinder tagFinder;

    protected abstract bool CanFire { get; }

    protected abstract UniTask Fire(CancellationToken cancellationToken);

    public async UniTaskVoid FireAutomatically(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await UniTask.WaitUntil(() => CanFire, cancellationToken: cancellationToken);
            await Fire(cancellationToken);
        }
    }
}
