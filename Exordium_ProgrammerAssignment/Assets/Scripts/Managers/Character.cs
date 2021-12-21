using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IncludeCaracterStats;

public class Character : MonoBehaviour
{
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Dexterity;
    public CharacterStat Luck;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;


    private void Awake()
    {
        statPanel.SetStats(Strength, Agility, Intelligence, Dexterity, Luck);
        statPanel.UpdateStatValues();

        inventory.OnItemRightClickedEvent += EquipFromInventory;
    }

    private void EquipFromInventory(Item item)
    {
        if(item is EquppableItem)
        {
            Equip((EquppableItem)item);
        }
    }

    public void Equip(EquppableItem item)
    {
        if(inventory.RemoveItem(item))
        {
            EquppableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquppableItem item)
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
 }
