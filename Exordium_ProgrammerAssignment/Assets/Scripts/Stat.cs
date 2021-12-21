using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public string statName;
    public float startValue;
    public float CurentValue;
    public float CurentBonusPercentageValue;
    public float BuffDurationValue;

    public enum StatType {
        MainStat,   
        Bonuses,     
        Buffs,
        Changeable,  

    }
    
}
