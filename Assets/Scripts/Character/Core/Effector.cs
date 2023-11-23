using System;
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

    public event Action<bool> StunRefreshed;
    public event Action<float> DamageBuffRefreshed;
    public event Action<float> DamageReductionRefreshed;

    public void Purify()
    {
        stunEffectIDs.Clear();
        RefreshStun();
    }

    public async UniTask ApplyStun(float duration)
    {
        var effectID = SetStun();
        await BattleTime.WaitForSeconds(duration, health.GetCancellationTokenOnDestroy());
        ClearStun(effectID);
    }

    public async UniTask ApplyDamageBuff(float multiplier, float duration)
    {
        var effectID = SetDamageBuff(multiplier);
        await BattleTime.WaitForSeconds(duration, health.GetCancellationTokenOnDestroy());
        ClearDamageBuff(effectID);
    }

    public async UniTask ApplyDamageReduction(float multiplier, float duration)
    {
        var effectID = SetDamageReduction(multiplier);
        await BattleTime.WaitForSeconds(duration, health.GetCancellationTokenOnDestroy());
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
        var isStunned = stunEffectIDs.Count > 0;
        movement.enabled = !isStunned;
        weapon.enabled = !isStunned;
        skill.enabled = !isStunned;
        StunRefreshed?.Invoke(isStunned);
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
        DamageBuffRefreshed?.Invoke(weapon.DamageMultiplier);
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
        DamageReductionRefreshed?.Invoke(health.DamageReduction);
    }

    private void ClearDamageReduction(int effectID)
    {
        damageReductionEffectIDs.Remove(effectID);
        RefreshDamageReduction();
    }

    private int NewEffectID() => newEffectID++;

    private float Multiply(float a, float b) => a * b;
}
