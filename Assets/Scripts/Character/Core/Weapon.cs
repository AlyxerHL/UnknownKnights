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
    private CharacterFinder finder;

    private CancellationTokenSource cancellation;

    private bool CanFire =>
        finder.Tag != null
        && (transform.position - finder.Tag.transform.position).sqrMagnitude <= range;

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
                finder.Tag.Health.GetDamaged(damage);
                await UniTask.Delay(recoveryTime, cancellationToken: cancellation.Token);
            }
        }
    }

    public void StopFiring()
    {
        cancellation?.Cancel();
    }
}
