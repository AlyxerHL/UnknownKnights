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

    public int ApplyStun()
    {
        var effectID = NewEffectID();
        stunEffectIDs.Add(effectID);
        RefreshStun();
        return effectID;
    }

    public void RemoveStun(int effectID)
    {
        stunEffectIDs.Remove(effectID);
        RefreshStun();
    }

    public int ApplyDamageBuff(float multiplier)
    {
        var effectID = NewEffectID();
        damageBuffEffectIDs.Add(effectID, multiplier);
        RefreshDamageBuff();
        return effectID;
    }

    public void RemoveDamageBuff(int effectID)
    {
        damageBuffEffectIDs.Remove(effectID);
        RefreshDamageBuff();
    }

    public int ApplyDamageReduction(float multiplier)
    {
        var effectID = NewEffectID();
        damageReductionEffectIDs.Add(effectID, multiplier);
        RefreshDamageReduction();
        return effectID;
    }

    public void RemoveDamageReduction(int effectID)
    {
        damageReductionEffectIDs.Remove(effectID);
        RefreshDamageReduction();
    }

    private void RefreshStun()
    {
        Movement.enabled = stunEffectIDs.Count == 0;
        Weapon.enabled = stunEffectIDs.Count == 0;
        Skill.enabled = stunEffectIDs.Count == 0;
    }

    private void RefreshDamageBuff()
    {
        Weapon.DamageMultiplier = damageBuffEffectIDs.Values.Aggregate(1f, Multiply);
    }

    private void RefreshDamageReduction()
    {
        Health.DamageReduction = damageReductionEffectIDs.Values.Aggregate(1f, Multiply);
    }

    private int NewEffectID() => newEffectID++;

    private float Multiply(float a, float b) => a * b;
}
