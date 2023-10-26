using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(TargetTagFinder))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float damage = 30f;

    [SerializeField]
    protected float sqrRange = 1f;

    [SerializeField]
    private int cooldown = 500;

    protected TargetTagFinder targeter;
    private CancellationTokenSource cancellation;

    private bool CanAttack =>
        targeter.TargetTag == null
        || (transform.position - targeter.TargetTag.transform.position).sqrMagnitude > sqrRange;

    private void Awake()
    {
        targeter = GetComponent<TargetTagFinder>();
        cancellation = new CancellationTokenSource();
    }

    private void OnEnable()
    {
        Attack(cancellation.Token).Forget();
    }

    private void OnDisable()
    {
        cancellation.Cancel();
    }

    private async UniTaskVoid Attack(CancellationToken cancellationToken)
    {
        while (true)
        {
            if (!CanAttack)
            {
                await UniTask.NextFrame(cancellationToken: cancellationToken);
                continue;
            }

            targeter.TargetTag.Health.TakeDamage(damage);
            await UniTask.Delay(cooldown, cancellationToken: cancellationToken);
        }
    }

    public abstract void Use();
}
