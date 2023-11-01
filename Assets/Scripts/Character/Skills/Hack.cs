using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Hack : Skill
{
    [SerializeField]
    private float range = 15f;

    [SerializeField]
    private float effectDuration = 1.5f;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    private bool IsWithinRange =>
        (transform.position - finder.Character.transform.position).sqrMagnitude <= range;

    protected override UniTask Use(CancellationToken _)
    {
        if (finder.Character == null || !IsWithinRange)
        {
            return UniTask.CompletedTask;
        }

        finder.Character.Effector.ApplyStun(effectDuration).Forget();
        return UniTask.CompletedTask;
    }
}
