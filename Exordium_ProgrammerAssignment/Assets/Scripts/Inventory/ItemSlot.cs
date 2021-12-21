﻿
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] Image Image ;

    public event Action<Item> OnRightClickEvent;
    private Item item;
    public Item Item
    {
        get { return item; }
        set { 
            
        item = value;
        if(item == null)
            {               
                Image.enabled = false;
            }
            else
            {
                Image.sprite = item.Image;
                Image.enabled = true;
            }
            
        
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if(Item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(Item);
            } 
        }
    }

    protected virtual void OnValidate()
    {
        if(Image == null)
        {           
            Image = GetComponent<Image>();
        }
    }
}
