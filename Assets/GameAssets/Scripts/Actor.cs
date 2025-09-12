using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Actor : MonoBehaviour
{
    public enum AttackOutcome
    {
        Miss,
        Blocked,
        Hit
    }

    private readonly Dictionary<ActorTypeSO, int> actorTypeLevels = new();
    private readonly List<AbilityAbstract> currentAbilities = new();

    private Attributes attributes;

    private int bonusDamage;

    private WeaponSO currentWeapon;
    private int defaultWeaponDamage;
    private WeaponSO droppedWeapon;
    public Actor target { get; private set; }
    public int health { get; private set; }
    public int maxHealth { get; private set; }
    public int currentMove { get; private set; }

    public void Initialize(Attributes attributes, ActorTypeSO actorType, GameObject enemyGO)
    {
        this.attributes = attributes;
        AddActorType(actorType);


        if (enemyGO.TryGetComponent<Actor>(out var target)) this.target = target;
        else Debug.LogError("No enemy actor");
    }

    private void InitializeActorType(ActorTypeSO actorType)
    {
        currentWeapon = actorType.defaultWeapon;
        defaultWeaponDamage = actorType.defaultWeaponDamage;
        attributes.Add(actorType.defaultAttributes);
        maxHealth += actorType.defaultHealth;
        droppedWeapon = actorType.droppedWeapon;
        if (currentWeapon == null) currentWeapon = actorType.defaultWeapon;
        foreach (var ability in actorType.defaultAbilities) AddAbility(ability);
    }

    public void AddBonusDamage(int amount)
    {
        bonusDamage += amount;
    }

    public AttackOutcome TryDealDamage(Actor target, out int damage)
    {
        damage = 0;
        if (Random.Range(1, attributes.agility + target.attributes.agility + 1) <= target.attributes.agility)
            return AttackOutcome.Miss;

        damage = currentWeapon.damage + defaultWeaponDamage + attributes.strength;
        ApplyAbilities(AbilityContext.Combat);
        damage += bonusDamage;
        return AttackOutcome.Hit;
    }

    private void ApplyAbilities(AbilityContext context)
    {
        foreach (var ability in currentAbilities) ability.TryApply(this, context);
    }

    public void ModifyAttributes(Attributes addedAttributes)
    {
        attributes.Add(addedAttributes);
    }

    public Attributes GetAttributes()
    {
        return attributes;
    }

    public void AddAbility(AbilityAbstract referenceAbility)
    {
        var ability = referenceAbility.CreateInstance();
        currentAbilities.Add(ability);
        ability.TryApply(this, AbilityContext.Init);
    }

    public void AddActorType(ActorTypeSO actorType)
    {
        if (actorTypeLevels.TryAdd(actorType, 1))
        {
        }

        if (actorType.TryGetLevelUpReward(actorTypeLevels[actorType] + 1, out var reward))
        {
            actorTypeLevels[actorType]++;
            ApplyLevelUpReward(reward);
        }
    }

    private void ApplyLevelUpReward(LevelUpReward reward)
    {
        foreach (var ability in reward.newAbilities) AddAbility(ability);
        ModifyAttributes(reward.attributeBonus);
        health += reward.healthReward;
        health += reward.attributeBonus.endurance;
    }
}