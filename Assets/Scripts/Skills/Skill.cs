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
            if (!CanUse)
            {
                await UniTask.NextFrame(cancellationToken);
                continue;
            }

            await Use(cancellationToken);
            await UniTask.Delay(cooldown, cancellationToken: cancellationToken);
        }
    }

    protected abstract UniTask Use(CancellationToken cancellationToken);
}
