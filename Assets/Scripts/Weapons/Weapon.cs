using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float damage;

    [SerializeField]
    protected float range;

    [SerializeField]
    private int cooldown;

    protected TargetTag targetTag;
    private CancellationTokenSource autoFire;

    private bool CanAttack =>
        targetTag != null
        && (transform.position - targetTag.transform.position).sqrMagnitude > range;

    private void Awake()
    {
        autoFire = new CancellationTokenSource();
        targetTag = TargetTag.ActiveTargetTags.MinBy(
            (e) => (transform.position - e.transform.position).sqrMagnitude
        );
    }

    private void OnEnable()
    {
        AutoFire(autoFire.Token).Forget();
    }

    private void OnDisable()
    {
        autoFire.Cancel();
    }

    private async UniTaskVoid AutoFire(CancellationToken cancellationToken)
    {
        while (true)
        {
            if (!CanAttack)
            {
                await UniTask.NextFrame(cancellationToken: cancellationToken);
                continue;
            }

            targetTag.Health.TakeDamage(damage);
            await UniTask.Delay(cooldown, cancellationToken: cancellationToken);
        }
    }

    protected abstract void Fire();
}
