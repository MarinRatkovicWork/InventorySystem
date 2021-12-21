
using UnityEngine;
using IncludeCaracterStats;
public enum EquipmentType
{
    Helmet,
    Chest,
    Gloves,
    Boots,
    Weapon,
    Shild,
    Ring,
    Necklace,
}

[CreateAssetMenu]
public class EquppableItem : Item
{ 
    public EquipmentType EquipmentType;
    [Header("Bonus Attributes")]
    
    public int StrenghtBonus;
    public int AgilityBonus;
    public int IntelligenceBonus;
    public int DexterityBonus;
    public int LuckBonus;

    [Header("Percent Attributes")]
    public int StrenghtPercentBonus;
    public int AgilityPercentBonus;
    public int IntelligencePercentBonus;
    public int DexterityPercentBonus;
    public int LuckPercentBonus;

    public void Equip(Character c)
    {
        if(StrenghtBonus != 0)
        {          
            c.Strength.AddModifier(new StatModifier(StrenghtBonus,StatModType.Flat,this) );
        }
        if (AgilityBonus != 0)
        {
            c.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
        }
        if (IntelligenceBonus != 0)
        {
            c.Intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
        }
        if (DexterityBonus != 0)
        {
            c.Dexterity.AddModifier(new StatModifier(DexterityBonus, StatModType.Flat, this));
        }
        if (LuckBonus != 0)
        {
            c.Luck.AddModifier(new StatModifier(LuckBonus, StatModType.Flat, this));
        }
        
        if(StrenghtPercentBonus != 0)
        {          
            c.Strength.AddModifier(new StatModifier(StrenghtPercentBonus, StatModType.PercentMult,this) );
        }
        if (AgilityPercentBonus != 0)
        {
            c.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModType.PercentMult, this));
        }
        if (IntelligencePercentBonus != 0)
        {
            c.Intelligence.AddModifier(new StatModifier(IntelligencePercentBonus, StatModType.PercentMult, this));
        }
        if (DexterityPercentBonus != 0)
        {
            c.Dexterity.AddModifier(new StatModifier(DexterityPercentBonus, StatModType.PercentMult, this));
        }
        if (LuckPercentBonus != 0)
        {
            c.Luck.AddModifier(new StatModifier(LuckPercentBonus, StatModType.PercentMult, this));
        }
    }

    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Intelligence.RemoveAllModifiersFromSource(this);
        c.Luck.RemoveAllModifiersFromSource(this);
        c.Dexterity.RemoveAllModifiersFromSource(this);
    }
   
}
