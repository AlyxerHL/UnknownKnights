using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onBeginUse;

    [SerializeField]
    private UnityEvent onEndUse;

    [SerializeField]
    private int cooldown;

    private bool isCooldown = true;
    private CancellationTokenSource cancellation;

    public event UnityAction OnBeginUse
    {
        add => onBeginUse.AddListener(value);
        remove => onBeginUse.RemoveListener(value);
    }

    public event UnityAction OnEndUse
    {
        add => onEndUse.AddListener(value);
        remove => onEndUse.RemoveListener(value);
    }

    protected abstract bool CanUse { get; }

    // TODO: Replace with UI
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSkill().Forget();
        }
    }

    private void Start()
    {
        Cooldown().Forget();
    }

    private void OnDisable()
    {
        cancellation?.Cancel();
    }

    public async UniTask Cooldown()
    {
        isCooldown = true;
        await UniTask.Delay(cooldown);
        isCooldown = false;
    }

    public async UniTask UseSkill()
    {
        if (isCooldown || !CanUse)
        {
            return;
        }

        Cooldown().Forget();
        cancellation = new();
        onBeginUse.Invoke();
        await Use(cancellation.Token);
        onEndUse.Invoke();
    }

    protected abstract UniTask Use(CancellationToken cancellationToken);
}
