using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ActorTypeSO")]
public class ActorTypeSO : ScriptableObject
{
    public string typeName;
    public bool isPlayer;

    public Attributes defaultAttributes;
    public int defaultHealth;
    public int defaultWeaponDamage;
    public WeaponSO droppedWeapon;
    public WeaponSO defaultWeapon;
    [SerializeReference] public List<AbilityAbstract> defaultAbilities;
}


[Serializable]
public abstract class AbilityAbstract
{
    public bool IsActivated { get; protected set; }
    public abstract void Apply(Actor caller);

    public virtual string GetDebugName()
    {
        return "AbstractAbility";
    }
}

[Serializable]
public class AgilityBonus : AbilityAbstract
{
    public override string GetDebugName()
    {
        return "AgilityBonus";
    }

    public override void Apply(Actor caller)
    {
        Debug.Log("Applied!");
        caller.ModifyAttributes(0, 1, 0);
        Debug.Log(caller.GetAttributes().Agility);
    }
}

[Serializable]
public class StrengthBonus : AbilityAbstract
{
    private bool isApplied;

    public override void Apply(Actor caller)
    {
        Debug.Log("Applied strength!");
        caller.ModifyAttributes(1, 0, 0);
        Debug.Log(caller.GetAttributes().Agility);
    }
}