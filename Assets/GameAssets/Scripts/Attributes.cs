using System;

[Serializable]
public struct Attributes
{
    public int strength;
    public int agility;
    public int endurance;

    public Attributes(int strength, int agility, int endurance)
    {
        this.strength = strength;
        this.agility = agility;
        this.endurance = endurance;
    }

    public void Add(Attributes other)
    {
        strength += other.strength;
        agility += other.agility;
        endurance += other.endurance;
    }
}