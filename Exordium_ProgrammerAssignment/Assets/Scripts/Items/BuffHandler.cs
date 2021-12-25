using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffHandler : MonoBehaviour
{
    public GameObject player;
    float timePased ;
    private float elapsedTime;
    private int timeStop1;
    private int timeStop2;

    int saveStartValue;

    private int startBonusMode;
    private GameObject item;
    private PlayerStats PlayerStats;
    private Items itemData;
    
    private Image buffSlotImage;
    private bool childFound;
    void Start()
    {
        PlayerStats = player.GetComponent<PlayerStats>();
        timeStop1 = 0;
        timeStop2 = 0;
        item = null;
        Image buffSlotImage = this.GetComponent<Image>();
        startBonusMode = 1; 
        childFound = false;
        elapsedTime = 0;
        timePased = 0f;

    }
   
    void FixedUpdate()
    {
        switch (startBonusMode)
        {
            case 1:
                if (childFound == false)
                {
                    findCild();
                    
                    
                }
                else
                {
                    populateData();
                }

                break;
            case 2:
                
               
                break;
            case 3:
                BonuseMode3();
                break; 
            case 4:              
                BonuseMode4();
                break;
            default:

                break;
        }



    }
    private void findCild()
    {
        if (this.gameObject.transform.childCount > 0)
        {
            item = gameObject.transform.GetChild(0).gameObject;
            itemData = item.GetComponent<ItemControl>().itemData;
            childFound = true;
        }
    }
    private void populateData()
    {
        buffSlotImage = this.GetComponent<Image>();
        buffSlotImage.sprite = itemData.Artwork;
        buffSlotImage.color = new Color32(255, 255, 225, 255);
        buffSlotImage.fillAmount = 1;
        if (itemData.consumptionType == Items.ConsumptionType.AddBonusesDirectly)
        {
            Debug.Log("mode2");
            startBonusMode = 2;
        }
        else if (itemData.consumptionType == Items.ConsumptionType.HoldBonusValueOverTime)
        {
            Debug.Log("mode3");
            startBonusMode = 3;
        }
        else if (itemData.consumptionType == Items.ConsumptionType.RampValueUpAndDownOverTime)
        {
            Debug.Log("mode4");
            startBonusMode = 4;
        }
        else if (itemData.consumptionType == Items.ConsumptionType.ChangeValueOverTimeInTickManner)
        {
            Debug.Log("mode5");
            startBonusMode = 5;
        }
        else
        {
            Debug.Log("modeelse");
        }
    }
    private IEnumerator Wait(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
    }

    
    private void BonuseMode3()
    {
        float timeHold = itemData.itemBonusDuration;
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 1f)
        {
            timeStop1 = timeStop1 + 1;
            if (timeStop1 <= System.Convert.ToInt32(timeHold))
            {
                PlayerStats.CurentHelth = HoldBonusValueOverTime(PlayerStats.CurentHelth, itemData.ReplenishHelth);
                PlayerStats.CurentMana = HoldBonusValueOverTime(PlayerStats.CurentMana, itemData.ReplenishMana);
                PlayerStats.MaxHelth = HoldBonusValueOverTime(PlayerStats.MaxHelth, itemData.PermanentHelthIncrease);
                PlayerStats.MaxMana = HoldBonusValueOverTime(PlayerStats.MaxMana, itemData.PermanentManaIncrease);
                PlayerStats.Strenght = HoldBonusValueOverTime(PlayerStats.Strenght, itemData.Strenght);
                PlayerStats.Dexterity = HoldBonusValueOverTime(PlayerStats.Dexterity, itemData.Dexterity);
                PlayerStats.Agility = HoldBonusValueOverTime(PlayerStats.Agility, itemData.Agility);
                PlayerStats.Intelligence = HoldBonusValueOverTime(PlayerStats.Intelligence, itemData.Intelligence);
                PlayerStats.Luck = HoldBonusValueOverTime(PlayerStats.Luck, itemData.Luck);
                PlayerStats.Attack = HoldBonusValueOverTime(PlayerStats.Attack, itemData.Attack);
                PlayerStats.Defence = HoldBonusValueOverTime(PlayerStats.Defence, itemData.Defence);
                PlayerStats.UpdateStats();
            }
            else
            {
                PlayerStats.CurentHelth = HoldBonusValueOverTime(PlayerStats.CurentHelth, -itemData.ReplenishHelth);
                PlayerStats.CurentMana = HoldBonusValueOverTime(PlayerStats.CurentMana, -itemData.ReplenishMana);
                PlayerStats.MaxHelth = HoldBonusValueOverTime(PlayerStats.MaxHelth, -itemData.PermanentHelthIncrease);
                PlayerStats.MaxMana = HoldBonusValueOverTime(PlayerStats.MaxMana, -itemData.PermanentManaIncrease);
                PlayerStats.Strenght = HoldBonusValueOverTime(PlayerStats.Strenght, -itemData.Strenght);
                PlayerStats.Dexterity = HoldBonusValueOverTime(PlayerStats.Dexterity,- itemData.Dexterity);
                PlayerStats.Agility = HoldBonusValueOverTime(PlayerStats.Agility, -itemData.Agility);
                PlayerStats.Intelligence = HoldBonusValueOverTime(PlayerStats.Intelligence, -itemData.Intelligence);
                PlayerStats.Luck = HoldBonusValueOverTime(PlayerStats.Luck,- itemData.Luck);
                PlayerStats.Attack = HoldBonusValueOverTime(PlayerStats.Attack, -itemData.Attack);
                PlayerStats.Defence = HoldBonusValueOverTime(PlayerStats.Defence,-itemData.Defence);
                PlayerStats.UpdateStats();
                elapsedTime = 0;
                childFound = false;
                Destroy(item);
                startBonusMode = 1;
            }
            elapsedTime = 0;
        }
    }
    private int HoldBonusValueOverTime(int current, int increse)
    {
        int maxIncrese = current * increse / 100;
        return current + maxIncrese;

    }
    private void BonuseMode4()
    {
        elapsedTime += Time.deltaTime;
        float timeHold = itemData.itemBonusDuration; 
        float timeToMax = itemData.itemApplayBonusOverTime;       
        float combaindTime = timeHold + timeToMax;      
        float percentageComplate = timePased / combaindTime;
        Debug.Log("Perc: "+percentageComplate);
        Debug.Log("Combo"+combaindTime);
        Debug.Log("Past    " +timePased);
        buffSlotImage.fillAmount = buffSlotImage.fillAmount- percentageComplate;
        if (elapsedTime <= timeToMax)
        {

            PlayerStats.CurentHelth = RampValueUpAndDownOverTime(PlayerStats.CurentHelth, itemData.ReplenishHelth, timeToMax, elapsedTime);
            PlayerStats.CurentMana = RampValueUpAndDownOverTime(PlayerStats.CurentMana, itemData.ReplenishMana, timeToMax, elapsedTime);
            PlayerStats.MaxHelth = RampValueUpAndDownOverTime(PlayerStats.MaxHelth, itemData.PermanentHelthIncrease, timeToMax, elapsedTime);
            PlayerStats.MaxMana = RampValueUpAndDownOverTime(PlayerStats.MaxMana, itemData.PermanentManaIncrease, timeToMax, elapsedTime);
            PlayerStats.Strenght = RampValueUpAndDownOverTime(PlayerStats.Strenght, itemData.Strenght, timeToMax, elapsedTime);
            PlayerStats.Dexterity = RampValueUpAndDownOverTime(PlayerStats.Dexterity, itemData.Dexterity, timeToMax, elapsedTime);
            PlayerStats.Agility = RampValueUpAndDownOverTime(PlayerStats.Agility, itemData.Agility, timeToMax, elapsedTime);
            PlayerStats.Intelligence = RampValueUpAndDownOverTime(PlayerStats.Intelligence, itemData.Intelligence, timeToMax, elapsedTime);
            PlayerStats.Luck = RampValueUpAndDownOverTime(PlayerStats.Luck, itemData.Luck, timeToMax, elapsedTime);
            PlayerStats.Attack = RampValueUpAndDownOverTime(PlayerStats.Attack, itemData.Attack, timeToMax, elapsedTime);
            PlayerStats.Defence = RampValueUpAndDownOverTime(PlayerStats.Defence, itemData.Defence, timeToMax, elapsedTime);
            PlayerStats.UpdateStats();
        }
        else if (elapsedTime >= combaindTime)
        {
            PlayerStats.CurentHelth = RampValueUpAndDownOverTime(PlayerStats.CurentHelth, -itemData.ReplenishHelth, timeToMax, elapsedTime);
            PlayerStats.CurentMana = RampValueUpAndDownOverTime(PlayerStats.CurentMana, -itemData.ReplenishMana, timeToMax, elapsedTime);
            PlayerStats.MaxHelth = RampValueUpAndDownOverTime(PlayerStats.MaxHelth, -itemData.PermanentHelthIncrease, timeToMax, elapsedTime);
            PlayerStats.MaxMana = RampValueUpAndDownOverTime(PlayerStats.MaxMana, -itemData.PermanentManaIncrease, timeToMax, elapsedTime);
            PlayerStats.Strenght = RampValueUpAndDownOverTime(PlayerStats.Strenght, -itemData.Strenght, timeToMax, elapsedTime);
            PlayerStats.Dexterity = RampValueUpAndDownOverTime(PlayerStats.Dexterity, -itemData.Dexterity, timeToMax, elapsedTime);
            PlayerStats.Agility = RampValueUpAndDownOverTime(PlayerStats.Agility, -itemData.Agility, timeToMax, elapsedTime);
            PlayerStats.Intelligence = RampValueUpAndDownOverTime(PlayerStats.Intelligence, -itemData.Intelligence, timeToMax, elapsedTime);
            PlayerStats.Luck = RampValueUpAndDownOverTime(PlayerStats.Luck, -itemData.Luck, timeToMax, elapsedTime);
            PlayerStats.Attack = RampValueUpAndDownOverTime(PlayerStats.Attack, -itemData.Attack, timeToMax, elapsedTime);
            PlayerStats.Defence = RampValueUpAndDownOverTime(PlayerStats.Defence, -itemData.Defence, timeToMax, elapsedTime);
            PlayerStats.UpdateStats();
            elapsedTime = 0;
            childFound = false;
            Destroy(item);
            startBonusMode = 1;
        }
        

             
    }
    private int RampValueUpAndDownOverTime(int current, int increse, float timeToMax,float seconds)
    {

        if (increse != 0)
        {           
            float returnValue = current;
            float amountToAdd = increse / timeToMax;          
            if (timeToMax <= seconds)
            {
                return current = current + System.Convert.ToInt32(amountToAdd);
            }
            else
            {
                return current+increse;
            }
            
        }
        return current;
    }

    private void BonuseMode5()
    {
        elapsedTime += Time.deltaTime;
        timePased += Time.deltaTime;
        float timeToMax = itemData.itemApplayBonusOverTime;
        float percentageComplate = timePased / timeToMax;
        buffSlotImage.fillAmount = buffSlotImage.fillAmount - percentageComplate;
        if (elapsedTime >= 1f)
        {

            timeStop1 = timeStop1 + 1;
            Debug.Log("timeStop1: " + timeStop1);
            if (timeStop1 <= System.Convert.ToInt32(timeToMax))
            {
                PlayerStats.CurentHelth = ChangeValueOverTimeInTickManner(PlayerStats.CurentHelth, itemData.ReplenishHelth, timeToMax, timeStop1);
                PlayerStats.CurentMana = ChangeValueOverTimeInTickManner(PlayerStats.CurentMana, itemData.ReplenishMana, timeToMax, timeStop1);
                PlayerStats.MaxHelth = ChangeValueOverTimeInTickManner(PlayerStats.MaxHelth, itemData.PermanentHelthIncrease, timeToMax, timeStop1);
                PlayerStats.MaxMana = ChangeValueOverTimeInTickManner(PlayerStats.MaxMana, itemData.PermanentManaIncrease, timeToMax, timeStop1);
                PlayerStats.Strenght = ChangeValueOverTimeInTickManner(PlayerStats.Strenght, itemData.Strenght, timeToMax, timeStop1);
                PlayerStats.Dexterity = ChangeValueOverTimeInTickManner(PlayerStats.Dexterity, itemData.Dexterity, timeToMax, timeStop1);
                PlayerStats.Agility = ChangeValueOverTimeInTickManner(PlayerStats.Agility, itemData.Agility, timeToMax, timeStop1);
                PlayerStats.Intelligence = ChangeValueOverTimeInTickManner(PlayerStats.Intelligence, itemData.Intelligence, timeToMax, timeStop1);
                PlayerStats.Luck = ChangeValueOverTimeInTickManner(PlayerStats.Luck, itemData.Luck, timeToMax, timeStop1);
                PlayerStats.Attack = ChangeValueOverTimeInTickManner(PlayerStats.Attack, itemData.Attack, timeToMax, timeStop1);
                PlayerStats.Defence = ChangeValueOverTimeInTickManner(PlayerStats.Defence, itemData.Defence, timeToMax, timeStop1);
                PlayerStats.UpdateStats();
            }
            else if (timeStop1 > System.Convert.ToInt32(timeToMax))
            {
                
                    elapsedTime = 0;
                    childFound = false;
                    Destroy(item);
                    startBonusMode = 1;                
            }

            elapsedTime = 0;
        }
    }


    private int ChangeValueOverTimeInTickManner(int current, int increse, float timeToMax, int seconds)
    {
        if (increse != 0)
        {
            int time = System.Convert.ToInt32(timeToMax);
            int returnValue = current;
            int amountToAdd = increse / time;
            int remainderToAdd = increse % time;
            if (time < seconds)
            {
                return current = current + amountToAdd;
            }
            else if (time == seconds)
            {
                return current = current + remainderToAdd;
            }
            return current;
        }
        return current;
    }
}
