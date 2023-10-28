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

    public bool IsCooldown { get; private set; } = true;

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
        Cooldown().Forget();
    }

    private void OnDisable()
    {
        cancellation.Cancel();
    }

    public async UniTask Cooldown()
    {
        IsCooldown = true;
        await UniTask.Delay(cooldown);
        IsCooldown = false;
    }

    public async UniTask UseSkill()
    {
        if (IsCooldown)
        {
            return;
        }

        Cooldown().Forget();
        onBeginUse.Invoke();
        await Use(cancellation.Token);
        onEndUse.Invoke();
    }

    protected abstract UniTask Use(CancellationToken cancellationToken);
}
