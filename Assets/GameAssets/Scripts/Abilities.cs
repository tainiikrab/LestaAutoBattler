using System;
using UnityEngine;

[Serializable]
public abstract class AbilityAbstract
{
    public bool isActivated { get; protected set; }
    public abstract void Apply(Actor caller);

    public virtual void Remove(Actor caller)
    {
        Debug.Log($"No remove required for {GetType().Name}");
    }
    public abstract AbilityAbstract Clone();
    // public virtual string GetDebugName()
    // {
    //     return "AbstractAbility";
    // }
}

[Serializable]
public class AgilityBonus : AbilityAbstract
{
    public override AbilityAbstract Clone()
    {
        return new AgilityBonus();
    }
    
    public override void Apply(Actor caller)
    {
        if (isActivated) return;
        isActivated = true;
        // Debug.Log("Applied!");
        caller.ModifyAttributes(0, 1, 0);
        // Debug.Log(caller.GetAttributes().Agility);
    }
}

[Serializable]
public class StrengthBonus : AbilityAbstract
{
    private bool isApplied;
    public override AbilityAbstract Clone()
    {
        return new StrengthBonus();
    }
    public override void Apply(Actor caller)
    {
        Debug.Log("Applied strength!");
        caller.ModifyAttributes(1, 0, 0);
        Debug.Log(caller.GetAttributes().Agility);
    }
}