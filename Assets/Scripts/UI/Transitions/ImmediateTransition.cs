using Cysharp.Threading.Tasks;

public class ImmediateTransition : Transition
{
    public override UniTask Show()
    {
        gameObject.SetActive(true);
        return UniTask.CompletedTask;
    }

    public override UniTask Hide()
    {
        gameObject.SetActive(false);
        return UniTask.CompletedTask;
    }
}
