using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject InventoryButoon;
    public GameObject EquipmentButoon;
    public GameObject AttributesButoon;
    public GameObject InventoryPanel;
    public GameObject EquipmentPanel;
    public GameObject AttributesPanel;

    public List<Items> items = new List<Items>();
    public GameObject item;
    private void Awake()
    {
        GameObject Equipment = GameObject.Find("Equipment").gameObject;
        Equipment.SetActive(false);
        GameObject Inventory = GameObject.Find("Inventory").gameObject;
        Inventory.SetActive(false);
        GameObject Atrtributes = GameObject.Find("Atrtributes").gameObject;
        Atrtributes.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        Schorcats();
    }
    private void Schorcats()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpownItems();
        }
        else

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryPanel.active == true)
            {
                InventoryPanel.SetActive(false);
                InventoryButoon.SetActive(true);
            }
            else
            {
                InventoryPanel.SetActive(true);
                InventoryButoon.SetActive(false);
            }
        }
        else
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (EquipmentPanel.active == true)
            {
                EquipmentPanel.SetActive(false);
                EquipmentButoon.SetActive(true);
            }
            else
            {
                EquipmentPanel.SetActive(true);
                EquipmentButoon.SetActive(false);
            }
        }
        else
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (AttributesPanel.active == true)
            {
                AttributesPanel.SetActive(false);
                AttributesButoon.SetActive(true);

            }
            else
            {
                AttributesPanel.SetActive(true);
                AttributesButoon.SetActive(false);
            }
        }
    }


    public void SpownItems()
    {

        for (int i = 0; i < items.Count; i++)
        {
            Instantiate(item, new Vector3(Random.Range(-32, 32) / 5.0F, Random.Range(23, -23) / 5.0F, Random.Range(-1, 0) / 5.0F), Quaternion.identity);
            item.GetComponent<ItemControl>().itemData = items[i];
        }

    }
}
