using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemSlotContanier;
    [SerializeField] ItemSlot[] itemsSlots;

    public event Action<Item> OnItemRightClickedEvent;

    private void Awake()
    {
        for(int i = 0;i< itemsSlots.Length; i++)
        {
            itemsSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }
    private void OnValidate()
    {
        if(itemSlotContanier != null)
        {
            itemsSlots = itemSlotContanier.GetComponentsInChildren<ItemSlot>();
        }

        RefreshUi();
    }

    private void RefreshUi()
    {
        int i = 0;
        for(; i<items.Count && i <itemsSlots.Length; i++)
        {
            itemsSlots[i].Item = items[i];

        }

        for (; i < itemsSlots.Length; i++)
        {
            itemsSlots[i].Item = null;

        }
    }

    public bool AddItem( Item item)
    {
        if (IsFull())
        {
            return false;
        }
        else
        {
            items.Add(item);
            RefreshUi();
            return true;
        }
    }
    
    public bool RemoveItem( Item item)
    {
        if (items.Remove(item))
        {
            RefreshUi();
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return items.Count >= itemsSlots.Length;
    }
}
