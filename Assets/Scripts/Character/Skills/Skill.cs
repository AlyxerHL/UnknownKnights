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
    private CancellationTokenSource skillCancellation;
    private CancellationTokenSource autoSkillCancellation;

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

    private void Start()
    {
        Cooldown().Forget();
        StartAutoSkill().Forget();
    }

    private void OnDisable()
    {
        CancelSkill();
        StopAutoSkill();
    }

    public async UniTask Cooldown()
    {
        isCooldown = true;
        await UniTask.Delay(cooldown);
        isCooldown = false;
    }

    public async UniTask UseSkill()
    {
        Debug.Log($"{name} used skill!");

        Cooldown().Forget();
        skillCancellation = new();
        onBeginUse.Invoke();
        await Use(skillCancellation.Token);
        onEndUse.Invoke();
    }

    public void CancelSkill()
    {
        skillCancellation?.Cancel();
    }

    public async UniTask StartAutoSkill()
    {
        autoSkillCancellation = new();

        while (!autoSkillCancellation.Token.IsCancellationRequested)
        {
            await UniTask.WaitWhile(
                () => isCooldown,
                cancellationToken: autoSkillCancellation.Token
            );

            await UseSkill();
        }
    }

    public void StopAutoSkill()
    {
        autoSkillCancellation?.Cancel();
    }

    protected abstract UniTask Use(CancellationToken cancellationToken);
}
