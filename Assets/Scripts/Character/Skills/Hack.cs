using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Hack : Skill
{
    [SerializeField]
    private NearestEnemyFinder finder;

    protected override UniTask Use(CancellationToken cancellationToken)
    {
        // TODO: 적 스턴 걸기
        return UniTask.CompletedTask;
    }
}
