using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Hack : Skill
{
    [SerializeField]
    private float range = 15f;

    [SerializeField]
    private int effectDuration = 1500;

    [SerializeField]
    private NearestEnemyCharacterFinder finder;

    private bool IsWithinRange =>
        (transform.position - finder.Tag.transform.position).sqrMagnitude <= range;

    protected override async UniTask Use(CancellationToken cancellationToken)
    {
        if (finder.Tag == null || !IsWithinRange)
        {
            return;
        }

        var effector = finder.Tag.Effector;
        var effectID = effector.SetStun();
        await UniTask.Delay(effectDuration, cancellationToken: cancellationToken);

        if (effector != null)
        {
            effector.ClearStun(effectID);
        }
    }
}
