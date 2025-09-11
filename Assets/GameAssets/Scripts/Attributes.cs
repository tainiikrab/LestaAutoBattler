using System;

[Serializable]
public struct Attributes
{
    public int Strength;
    public int Agility;
    public int Endurance;

    public Attributes(int strength, int agility, int endurance)
    {
        Strength = strength;
        Agility = agility;
        Endurance = endurance;
    }
}