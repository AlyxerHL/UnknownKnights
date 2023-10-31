using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effector : MonoBehaviour
{
    [SerializeField]
    private Health health;

    [SerializeField]
    private Movement movement;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private Skill skill;

    private readonly HashSet<int> stunEffectIDs = new();
    private readonly Dictionary<int, float> damageBuffEffectIDs = new();
    private readonly Dictionary<int, float> damageReductionEffectIDs = new();
    private int newEffectID = 0;

    public int SetStun()
    {
        var effectID = NewEffectID();
        stunEffectIDs.Add(effectID);
        ApplyStun();
        return effectID;
    }

    public void ClearStun(int effectID)
    {
        stunEffectIDs.Remove(effectID);
        ApplyStun();
    }

    public int SetDamageBuff(float multiplier)
    {
        var effectID = NewEffectID();
        damageBuffEffectIDs.Add(effectID, multiplier);
        ApplyDamageBuff();
        return effectID;
    }

    public void ClearDamageBuff(int effectID)
    {
        damageBuffEffectIDs.Remove(effectID);
        ApplyDamageBuff();
    }

    public int SetDamageReduction(float multiplier)
    {
        var effectID = NewEffectID();
        damageReductionEffectIDs.Add(effectID, multiplier);
        ApplyDamageReduction();
        return effectID;
    }

    public void ClearDamageReduction(int effectID)
    {
        damageReductionEffectIDs.Remove(effectID);
        ApplyDamageReduction();
    }

    private void ApplyStun()
    {
        movement.enabled = stunEffectIDs.Count == 0;
        weapon.enabled = stunEffectIDs.Count == 0;
        skill.enabled = stunEffectIDs.Count == 0;
    }

    private void ApplyDamageBuff()
    {
        weapon.DamageMultiplier = damageBuffEffectIDs.Values.Aggregate(1f, Multiply);
    }

    private void ApplyDamageReduction()
    {
        health.DamageReduction = damageReductionEffectIDs.Values.Aggregate(1f, Multiply);
    }

    private int NewEffectID() => newEffectID++;

    private float Multiply(float a, float b) => a * b;
}
