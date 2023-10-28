using Cysharp.Threading.Tasks;

public class PausePage : Page
{
    public override async UniTask Hide()
    {
        await UniTask.CompletedTask;
    }

    public override async UniTask Show()
    {
        await UniTask.CompletedTask;
    }
}
