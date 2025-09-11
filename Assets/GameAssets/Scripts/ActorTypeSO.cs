using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ActorTypeSO")]
public abstract class ActorTypeSO : ScriptableObject
{
    public string typeName;
    public bool isPlayer;
    
    public Attributes defaultAttributes;
    public int defaultHealth;

    public abstract int CalculateDamage();
    
    [SerializeReference] public List<AbilityAbstract> defaultAbilities;

}

[CreateAssetMenu(menuName = "ScriptableObjects/ActorType/PlayerClassSO")]
public class PlayerClassSO : ActorTypeSO
{
    public new bool isPlayer = true;
    public WeaponSO defaultWeapon;
    public List<LevelUpReward> LevelUpReward;
}

[CreateAssetMenu(menuName = "ScriptableObjects/ActorType/MobTypeSO")]
public class MobTypeSO : ActorTypeSO
{
    public int defaultWeaponDamage;
    public WeaponSO droppedWeapon;
}


[Serializable]
public struct LevelUpReward
{
    public int level;
    public List<AbilityAbstract> newAbilities;
    public Attributes attributeBonus;
}