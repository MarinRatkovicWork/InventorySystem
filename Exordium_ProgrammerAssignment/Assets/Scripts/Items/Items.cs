using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Items", menuName = "Itame")]
public class Items : ScriptableObject
{
    
    public enum ItemType
    {
        PickUpAble,
        PermanentUsage,    
        CutScene,
    }   
    public ItemType itemType;
    public enum UsageType
    {      
        Consumable,
        Equpment,        
        Collectible,
    }
    public UsageType usageType;
    [SerializeField]
    public enum Stackable
    {
        UnStackable,
        stackableLimited,
        stackableUnlimited,
    }
    public Stackable stackable;
    public enum Equpment 
    { 
        
        Hed,
        Torso,
        Wepon,
        Shild,
        Boots,
        Ring,
        
    } 
    public Equpment equpment;
    
    public enum ConsumptionType
    {
        AddBonusesDirectly,
        HoldBonusValueOverTime,
        RampValueUpAndDownOverTime,
        ChangeValueOverTimeInTickManner,

    } 
    
    public ConsumptionType consumptionType;
    [Space]
    public string ItameName;
    public Sprite Artwork;
    public int Durability;
    [Header("ConsumptionPerTime")]
   
    public float itemBonusDuration;  
    public float itemApplayBonusOverTime;   
    [Header("Bonus to stats")]
    public int PermanentManaIncrease;
    public int PermanentHelthIncrease;
     [Space]
    
    public int Strenght;
    public int Dexterity;
    public int Agility;
    public int Intelligence;   
    public int Luck;
    [Space]
    public int Attack;
    public int Defence;
    public int ReplenishMana;
    public int ReplenishHelth;        











}
