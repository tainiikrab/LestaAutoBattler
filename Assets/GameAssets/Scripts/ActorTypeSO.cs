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
public struct LevelUpReward
{
    public int level;
    public List<AbilityAbstract> newAbilities;
    public Attributes attributeBonus;
}