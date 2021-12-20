using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using IncludeCaracterStats;

public class PlayerStats : MonoBehaviour
{
    //public ChracterStat Strenght;

    public Slider sliderHelth;
    public Slider sliderMana;
    public Text ManaMax;  
    public Text ManaCurent;
    public Text HealthMax;
    public Text HealthCurent;

    public Text HealthMaxAtributesText; 
    public Text ManaMaxAtrinutesText;
    public Text AttackText;
    public Text DefenceTextt;

    public Text StrenghtText;
    public Text DexterityText;
    public Text AgilityText;
    public Text IntelligenceText;
    
    public Text StrenghtTextBonus;
    public Text DexterityTextBonus;
    public Text AgilityTextBonus;
    public Text IntelligenceTextBonus;

    public int MaxMana;
    public int CurentMana;

    public int MaxHelth;
    public int CurentHelth;

    //public int Strenght;
    public int Dexterity;
    public int Agility;
    public int Intelligence;

    public int Attack;
    public int Defence;

    private int AtckBonus;
    private int DfcBonus;
    private int HelthBonus;
    private int ManaBonus;

    void Start()
    {
        UpdateStats();


    }


    public void UpdateStats()
    {       
     AtckBonus = Strenght * 2;
     DfcBonus = Dexterity * 3;
     HelthBonus = Agility * 10;
     ManaBonus = Intelligence * 10;

     HealthMaxAtributesText.text = MaxHelth.ToString()+"+"+HelthBonus.ToString();
     ManaMaxAtrinutesText.text = MaxMana.ToString()+"+"+ManaBonus.ToString();
     AttackText.text=Attack.ToString()+"+"+AtckBonus.ToString();
     DefenceTextt.text=Defence.ToString()+"+"+DfcBonus.ToString();

     StrenghtText.text=Strenght.ToString();
     DexterityText.text= Dexterity.ToString();
     AgilityText.text = Agility.ToString();
     IntelligenceText.text = Intelligence.ToString();
   
     StrenghtTextBonus.text ="+"+ AtckBonus.ToString();      
     DexterityTextBonus.text = "+" + DfcBonus.ToString();       
     AgilityTextBonus.text = "+" + HelthBonus.ToString();     
     IntelligenceTextBonus.text= "+" + ManaBonus.ToString();

        sliderHelth.maxValue = MaxHelth;
        sliderMana.maxValue = MaxMana;

        
        sliderHelth.value=CurentHelth;
        sliderMana.value=CurentMana;

        HealthMax.text = sliderHelth.maxValue.ToString();
        HealthCurent.text = sliderHelth.value.ToString();
        ManaCurent.text = sliderMana.value.ToString();
        ManaMax.text = sliderMana.maxValue.ToString();       
        
    }
    
}
