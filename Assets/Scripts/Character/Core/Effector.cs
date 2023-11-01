using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

public class Effector
{
    private readonly Health health;
    private readonly Movement movement;
    private readonly Weapon weapon;
    private readonly Skill skill;

    private readonly HashSet<int> stunEffectIDs = new();
    private readonly Dictionary<int, float> damageBuffEffectIDs = new();
    private readonly Dictionary<int, float> damageReductionEffectIDs = new();
    private int newEffectID = 0;

    public Effector(Character character)
    {
        health = character.Health;
        movement = character.Movement;
        weapon = character.Weapon;
        skill = character.Skill;
    }

    public async UniTask ApplyStun(float duration)
    {
        var effectID = SetStun();
        await UniTask.WaitForSeconds(duration);
        ClearStun(effectID);
    }

    public async UniTask ApplyDamageBuff(float multiplier, float duration)
    {
        var effectID = SetDamageBuff(multiplier);
        await UniTask.WaitForSeconds(duration);
        ClearDamageBuff(effectID);
    }

    public async UniTask ApplyDamageReduction(float multiplier, float duration)
    {
        var effectID = SetDamageReduction(multiplier);
        await UniTask.WaitForSeconds(duration);
        ClearDamageReduction(effectID);
    }

    private int SetStun()
    {
        var effectID = NewEffectID();
        stunEffectIDs.Add(effectID);
        RefreshStun();
        return effectID;
    }

    private void RefreshStun()
    {
        movement.enabled = stunEffectIDs.Count == 0;
        weapon.enabled = stunEffectIDs.Count == 0;
        skill.enabled = stunEffectIDs.Count == 0;
    }

    private void ClearStun(int effectID)
    {
        stunEffectIDs.Remove(effectID);
        RefreshStun();
    }

    private int SetDamageBuff(float multiplier)
    {
        var effectID = NewEffectID();
        damageBuffEffectIDs.Add(effectID, multiplier);
        RefreshDamageBuff();
        return effectID;
    }

    private void RefreshDamageBuff()
    {
        weapon.DamageMultiplier = damageBuffEffectIDs.Values.Aggregate(1f, Multiply);
    }

    private void ClearDamageBuff(int effectID)
    {
        damageBuffEffectIDs.Remove(effectID);
        RefreshDamageBuff();
    }

    private int SetDamageReduction(float multiplier)
    {
        var effectID = NewEffectID();
        damageReductionEffectIDs.Add(effectID, multiplier);
        RefreshDamageReduction();
        return effectID;
    }

    private void RefreshDamageReduction()
    {
        health.DamageReduction = damageReductionEffectIDs.Values.Aggregate(1f, Multiply);
    }

    private void ClearDamageReduction(int effectID)
    {
        damageReductionEffectIDs.Remove(effectID);
        RefreshDamageReduction();
    }

    private int NewEffectID() => newEffectID++;

    private float Multiply(float a, float b) => a * b;
}
