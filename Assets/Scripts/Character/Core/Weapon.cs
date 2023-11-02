using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float range;

    [SerializeField]
    private float recoveryTime;

    [SerializeField]
    private SingleCharacterFinder finder;

    private CancellationTokenSource cancellation;

    public float DamageMultiplier { get; set; } = 1f;

    private bool CanFire =>
        finder.Character != null
        && (transform.position - finder.Character.transform.position).sqrMagnitude <= range;

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
                finder.Character.Health.GetDamaged(damage * DamageMultiplier);
                await BattleTime.WaitForSeconds(recoveryTime, cancellation.Token);
            }
        }
    }

    public void StopFiring()
    {
        cancellation?.Cancel();
    }
}
