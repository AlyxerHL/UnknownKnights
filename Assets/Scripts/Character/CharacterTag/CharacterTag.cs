using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterTag : MonoBehaviour
{
    public static readonly List<CharacterTag> ActiveTags = new();

    [field: SerializeField]
    public Health Health { get; private set; }

    [field: SerializeField]
    public Movement Movement { get; private set; }

    [field: SerializeField]
    public Weapon Weapon { get; private set; }

    [field: SerializeField]
    public Skill Skill { get; private set; }

    private readonly HashSet<int> stunEffectIDs = new();
    private readonly Dictionary<int, float> damageBuffEffectIDs = new();
    private readonly Dictionary<int, float> damageReductionEffectIDs = new();
    private int newEffectID = 0;

    private void OnEnable()
    {
        ActiveTags.Add(this);
    }

    private void OnDisable()
    {
        ActiveTags.Remove(this);
    }

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
        Movement.enabled = stunEffectIDs.Count == 0;
        Weapon.enabled = stunEffectIDs.Count == 0;
        Skill.enabled = stunEffectIDs.Count == 0;
    }

    private void ApplyDamageBuff()
    {
        Weapon.DamageMultiplier = damageBuffEffectIDs.Values.Aggregate(1f, Multiply);
    }

    private void ApplyDamageReduction()
    {
        Health.DamageReduction = damageReductionEffectIDs.Values.Aggregate(1f, Multiply);
    }

    private int NewEffectID() => newEffectID++;

    private float Multiply(float a, float b) => a * b;
}
