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
    public List<LevelUpReward> levelUpRewards;

    public bool IsUpgradeAvailable(int newLevel)
    {
        return newLevel <= levelUpRewards.Count + 1;
    }

    public bool TryGetLevelUpReward(int newLevel, out LevelUpReward reward)
    {
        reward = new LevelUpReward();
        if (!IsUpgradeAvailable(newLevel)) return false;
        reward = levelUpRewards[newLevel - 2];
        return true;
    }
}


[Serializable]
public struct LevelUpReward
{
    public int level;
    public int healthReward;
    public Attributes attributeBonus;
    [SerializeReference] public List<AbilityAbstract> newAbilities;
}