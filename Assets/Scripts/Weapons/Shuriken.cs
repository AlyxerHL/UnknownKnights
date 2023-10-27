using Cysharp.Threading.Tasks;

public class Shuriken : Weapon
{
    protected override bool CanFire => throw new System.NotImplementedException();

    protected override UniTask<bool> Fire()
    {
        throw new System.NotImplementedException();
    }
}
