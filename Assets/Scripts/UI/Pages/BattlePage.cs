using Cysharp.Threading.Tasks;

public class BattlePage : Page
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
