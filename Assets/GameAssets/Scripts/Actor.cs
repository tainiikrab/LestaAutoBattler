using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Actor : MonoBehaviour
{
    protected List<AbilityAbstract> currentAbilities = new();
    
    protected Attributes attributes;

    public void ModifyAttributes(int strength, int agility, int endurance)
    {
        attributes.Strength += strength;
        attributes.Agility += agility;
        attributes.Endurance += endurance;
    }

    public Attributes GetAttributes()
    {
        return attributes;
    }

    public void AddAbility(AbilityAbstract ability)
    {
        currentAbilities.Add(ability);
    }

    public abstract void Initialize(Attributes attributes, ActorTypeSO actorType);
    public abstract int DealDamage(Actor target, int currentMove);
}

public class Player : Actor
{
    public override void Initialize(Attributes attributes, ActorTypeSO actorType)
    {
        throw new System.NotImplementedException();
    }
    public WeaponSO currentWeapon;
    public Dictionary<PlayerClassSO, int> playerClasses;
    
    public override int DealDamage(Actor target, int currentMove)
    {
        if (Random.Range(1, attributes.Agility + target.GetAttributes().Agility + 1) <= target.GetAttributes().Agility) return -1;
        var damage = currentWeapon.damage + attributes.Strength;
        foreach (var ability in currentAbilities) ability.Apply(this);
        Debug.Log(attributes.Agility);
        return damage;
    }
}