using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    protected TargetTagFinder tagFinder;

    [SerializeField]
    private int cooldown;

    public abstract UniTask Use(CancellationToken cancellationToken);
}
