﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DurabilityHandler : MonoBehaviour
{

    public GameObject EqupmentSlots;
    public GameObject ItemPreviewe;
    public GameObject Player;

    private int totalDistance;
    private int startDistance;
    List<GameObject> itemsInEqupment;
    void FixedUpdate()
    {

        totalDistance = Player.GetComponent<PlayerMovement>().totalDistanceInt;

        UpdateEqupmentList();
        ItemDurabilityUpdate();
    }
    void Start()
    {
        UpdateEqupmentList();
        startDistance = 0;
    }

    private void UpdateEqupmentList()
    {
        List<GameObject> containersEqupment = new List<GameObject>();
        itemsInEqupment = new List<GameObject>();

        for (int i = 0; i < EqupmentSlots.transform.childCount; i++)
        {
            containersEqupment.Add(EqupmentSlots.transform.GetChild(i).gameObject.transform.Find("GameObjectContainer").gameObject);

        }
        for (int i = 0; i < containersEqupment.Count; i++)
        {
            if (containersEqupment[i].transform.childCount >= 1)
            {
                itemsInEqupment.Add(containersEqupment[i].transform.GetChild(0).gameObject);
            }
        }
    }



    public void ItemDurabilityUpdate()
    {
        if (totalDistance > startDistance)
        {


            if (itemsInEqupment.Count >= 1)
            {
                for (int i = 0; i < itemsInEqupment.Count; i++)
                {
                    GameObject item = itemsInEqupment[i];
                    item.GetComponent<ItemControl>().currentDurability = item.GetComponent<ItemControl>().currentDurability - 1;                                 
                    if (item.GetComponent<ItemControl>().currentDurability == 0)
                    {

                        GameObject firstParant = item.transform.parent.gameObject;
                        GameObject lastParant = firstParant.transform.parent.gameObject;
                        GameObject image = lastParant.transform.Find("Image").gameObject;
                        image.GetComponent<Image>().sprite = null;
                        Destroy(item);
                    }

                }
            }
            startDistance = totalDistance;
        }
    }
}
