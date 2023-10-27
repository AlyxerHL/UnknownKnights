using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    private readonly CancellationTokenSource autoUse = new();

    [SerializeField]
    private int cooldown;

    [SerializeField]
    protected TargetTagFinder tagFinder;

    protected abstract bool CanUse { get; }

    private void OnEnable()
    {
        UseAutomatically(autoUse.Token).Forget();
    }

    private void OnDisable()
    {
        autoUse.Cancel();
    }

    private async UniTaskVoid UseAutomatically(CancellationToken cancellationToken)
    {
        while (true)
        {
            await UniTask.Delay(cooldown, cancellationToken: cancellationToken);
            await UniTask.WaitUntil(() => CanUse, cancellationToken: cancellationToken);
            await Use(cancellationToken);
        }
    }

    protected abstract UniTask Use(CancellationToken cancellationToken);
}
