using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private Skill skill;

    private CancellationTokenSource cancellation;

    // TODO: Replace with UI
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSkill().Forget();
        }
    }

    private void OnEnable()
    {
        cancellation = new();
        weapon.FireAutomatically(cancellation.Token).Forget();
        skill.Cooldown().Forget();
    }

    private void OnDisable()
    {
        cancellation.Cancel();
    }

    public async UniTask UseSkill()
    {
        if (skill.IsCooldown)
        {
            return;
        }

        cancellation.Cancel();
        cancellation = new();
        skill.Cooldown().Forget();
        await skill.Use(cancellation.Token);
        weapon.FireAutomatically(cancellation.Token).Forget();
    }
}
