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
    } 
    public Equpment equpment;
  
    public string ItameName;
    public int amount;  
    public Sprite Artwork;

    public int PermanentManaIncrease;
    public int PermanentHelthIncrease;
    public int ReplenishMana;
    public int ReplenishHelth;

    public int Strenght;
    public int Dexterity;
    public int Agility;
    public int Intelligence;   
    public int Luck;   
    public int Attack;
    public int Defence;
   
   









}
