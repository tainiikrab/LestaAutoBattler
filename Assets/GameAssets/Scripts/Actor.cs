using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Actor : MonoBehaviour
{
    private readonly List<AbilityAbstract> currentAbilities = new();
    private ActorTypeSO actorType;
    private Attributes attributes;
    private WeaponSO currentWeapon;

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

    public void Initialize(Attributes attributes, ActorTypeSO actorType)
    {
        this.attributes = attributes;
        this.actorType = actorType;
        currentWeapon = actorType.defaultWeapon;
        foreach (var ability in actorType.defaultAbilities) AddAbility(ability);
    }

    public int DealDamage(Actor target, int currentMove)
    {
        if (Random.Range(1, attributes.Agility + target.attributes.Agility + 1) <= target.attributes.Agility) return -1;
        var damage = currentWeapon.damage + actorType.defaultWeaponDamage + attributes.Strength;
        foreach (var ability in currentAbilities) ability.Apply(this);
        Debug.Log(attributes.Agility);
        return damage;
    }
}