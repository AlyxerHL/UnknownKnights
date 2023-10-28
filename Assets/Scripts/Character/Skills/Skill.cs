using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    protected TargetTagFinder tagFinder;

    [SerializeField]
    private int cooldown;

    public bool IsCooldown { get; private set; } = true;

    public async UniTask Cooldown()
    {
        IsCooldown = true;
        await UniTask.Delay(cooldown);
        IsCooldown = false;
    }

    public abstract UniTask Use(CancellationToken cancellationToken);
}
