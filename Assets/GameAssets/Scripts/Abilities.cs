using System;
using UnityEngine;

public enum AbilityContext
{
    Combat,
    Init
}

[Serializable]
public abstract class AbilityAbstract
{
    public bool isActivated { get; protected set; }
    public abstract bool TryApply(Actor caller, AbilityContext context);

    public virtual void Remove(Actor caller)
    {
        Debug.Log($"No remove required for {GetType().Name}");
    }

    // need clone due to the static nature of ScriptableObject 
    public abstract AbilityAbstract CreateInstance();
}

[Serializable]
public class AgilityBonus : AbilityAbstract
{
    public override AbilityAbstract CreateInstance()
    {
        return new AgilityBonus();
    }

    public override bool TryApply(Actor caller, AbilityContext context)
    {
        if (isActivated || context != AbilityContext.Init) return false;
        isActivated = true;
        caller.ModifyAttributes(new Attributes(0, 1, 0));
        return true;
    }
}

[Serializable]
public class HiddenAttack : AbilityAbstract
{
    public override AbilityAbstract CreateInstance()
    {
        return new HiddenAttack();
    }

    public override bool TryApply(Actor caller, AbilityContext context)
    {
        if (context != AbilityContext.Combat) return false;
        if (caller.GetAttributes().agility > caller.target.GetAttributes().agility) caller.AddBonusDamage(1);
        return true;
    }
}