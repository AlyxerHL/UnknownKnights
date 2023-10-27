using Cysharp.Threading.Tasks;

public class Shuriken : Weapon
{
    protected override bool CanFire => false;

    protected override async UniTask Fire()
    {
        await UniTask.CompletedTask;
    }
}
