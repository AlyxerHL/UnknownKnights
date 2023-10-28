using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float damage = 10f;

    [SerializeField]
    private float range = 4f;

    [SerializeField]
    private int recoveryTime = 680;

    [SerializeField]
    private TargetFinder finder;

    private CancellationTokenSource cancellation;

    private bool CanFire =>
        finder.TargetTag != null
        && (transform.position - finder.TargetTag.transform.position).sqrMagnitude <= range;

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
                await UniTask.WaitUntil(() => CanFire, cancellationToken: cancellation.Token);
                finder.TargetTag.Health.GetDamaged(damage);
                await UniTask.Delay(recoveryTime, cancellationToken: cancellation.Token);
            }
        }
    }

    public void StopFiring()
    {
        cancellation.Cancel();
    }
}
