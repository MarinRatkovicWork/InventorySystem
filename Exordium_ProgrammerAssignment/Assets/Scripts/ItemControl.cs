using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{

    public Items itemData;
    public GameObject Player;
    private PlayerStats PlayerStats;


    public void Start()
    {       
        gameObject.GetComponent<SpriteRenderer>().sprite = itemData.Artwork;   
        PlayerStats = Player.GetComponent<PlayerStats>();
    }
    public void ApplayBuffs()
    {
        PlayerStats.CurentHelth = PlayerStats.CurentHelth + itemData.ReplenishHelth;
        PlayerStats.CurentMana = PlayerStats.CurentMana + itemData.ReplenishMana;
        
        PlayerStats.MaxHelth = PlayerStats.MaxHelth + itemData.PermanentHelthIncrease;
        PlayerStats.MaxMana = PlayerStats.MaxMana + itemData.PermanentManaIncrease;
       
        PlayerStats.Strenght = PlayerStats.Strenght + itemData.Strenght;
        PlayerStats.Dexterity = PlayerStats.Dexterity + itemData.Dexterity;
        PlayerStats.Agility = PlayerStats.Agility + itemData.Agility;
        PlayerStats.Intelligence = PlayerStats.Intelligence + itemData.Intelligence;

        PlayerStats.Attack = PlayerStats.Attack + itemData.Attack;
        PlayerStats.Defence = PlayerStats.Defence + itemData.Defence;
        PlayerStats.UpdateStats();
    }
    
    public void RemoveBuffs()
    {
        PlayerStats.CurentHelth = PlayerStats.CurentHelth - itemData.ReplenishHelth;
        PlayerStats.CurentMana = PlayerStats.CurentMana - itemData.ReplenishMana;
        
        PlayerStats.MaxHelth = PlayerStats.MaxHelth - itemData.PermanentHelthIncrease;
        PlayerStats.MaxMana = PlayerStats.MaxMana - itemData.PermanentManaIncrease;
       
        PlayerStats.Strenght = PlayerStats.Strenght - itemData.Strenght;
        PlayerStats.Dexterity = PlayerStats.Dexterity - itemData.Dexterity;
        PlayerStats.Agility = PlayerStats.Agility - itemData.Agility;
        PlayerStats.Intelligence = PlayerStats.Intelligence - itemData.Intelligence;

        PlayerStats.Attack = PlayerStats.Attack - itemData.Attack;
        PlayerStats.Defence = PlayerStats.Defence - itemData.Defence;
        PlayerStats.UpdateStats();
    }
       
    
}
