using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [Serializable]
    public enum WeaponDamageType
    {
        Slashing,
        Blunting,
        Piercing
    }

    public int damage;
    public string weaponName;
    public WeaponDamageType damageType;
    public Sprite sprite;
}