using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public bool fallowStart = false;

    public GameObject ItemSlotContanier;
    List<GameObject> Slots = new List<GameObject>();
    private GameObject imageContainer;
    private GameObject GameObjectContainer;
    private GameObject ItemExistsInContainer;
    private GameObject NumberText;
    private bool ItemHasntBeanFaund;
    public GameObject EquipmentSlots;
  
    private void CheckNumberOfSlots()
    {
        for (int i = 0; i < ItemSlotContanier.transform.childCount; i++)
        {
            Slots.Add(ItemSlotContanier.transform.GetChild(i).gameObject);
        }
    }

    private string ReturnSlotName(Items.Equpment itemsEqupment)
    {
        string slotName;
        if (itemsEqupment == Items.Equpment.Hed)
        {
            slotName = "HeadSlot";
        }
        else if (itemsEqupment == Items.Equpment.Torso)
        {
            slotName = "TorsoSlot";
        }
        else if (itemsEqupment == Items.Equpment.Boots)
        {
            slotName = "BootsSlot";
        }
        else if (itemsEqupment == Items.Equpment.Shild)
        {
            slotName = "ShildSlot";
        }
        else if (itemsEqupment == Items.Equpment.Wepon)
        {
            slotName = "WeaponSlot";
        }
        else
        {
            slotName = "";
        }
        return slotName;
    }

    private (bool itemEquipped, GameObject gameObjectContainer) CheckIfItemTypeIsAlreadyEquipped(GameObject compareWith)
    {
        List<GameObject> EquipmentSlot = new List<GameObject>();
        for (int a = 0; a < EquipmentSlots.transform.childCount; a++)
        {
            EquipmentSlot.Add(EquipmentSlots.transform.GetChild(a).gameObject);
        }
        for (int a = 0; a < EquipmentSlot.Count; a++)
        {
            if (EquipmentSlot[a].name == ReturnSlotName(compareWith.GetComponent<ItemControl>().itemData.equpment))
            {
               GameObject GameObjectContainerEquipment = EquipmentSlot[a].transform.Find("GameObjectContainer").gameObject;
                if (GameObjectContainerEquipment.transform.childCount == 0)
                {

                    return (false, GameObjectContainerEquipment);

                }

            }
        }
        return (true, null);

    }

    private void MoveTransformItem(GameObject startPosition, GameObject endPosition)
    {
        startPosition.transform.parent = endPosition.transform;
        startPosition.transform.position = endPosition.transform.position;
        startPosition.gameObject.SetActive(false);
    }
    private void MoveSprites (GameObject startPosition, GameObject endPosition)
    {
        GameObject endPositionParent = endPosition.transform.parent.gameObject;
        GameObject ImageInEqupment = endPositionParent.transform.Find("Image").gameObject;
        ImageInEqupment.GetComponent<Image>().sprite = startPosition.gameObject.GetComponent<ItemControl>().itemData.Artwork;
    }

    private (bool itemExist, GameObject gameObjectContainer) CheckIfItemExistInInventoryAndStackable(GameObject itemToCheck)
    {       
        if (itemToCheck.gameObject.GetComponent<ItemControl>().itemData.stackable == Items.Stackable.UnStackable)
        {
            return (false,null);
        }
        else if (itemToCheck.gameObject.GetComponent<ItemControl>().itemData.stackable == Items.Stackable.stackableLimited)
        {
            for (int a = 0; Slots.Count > a; a++)
            {
                GameObjectContainer = Slots[a].transform.Find("GameObjectContainer").gameObject;
                if (GameObjectContainer.transform.childCount > 0 && GameObjectContainer.transform.childCount < 5)
                {
                    ItemExistsInContainer = GameObjectContainer.transform.GetChild(0).gameObject;
                    if (ItemExistsInContainer.GetComponent<ItemControl>().itemData.ItameName == itemToCheck.GetComponent<ItemControl>().itemData.ItameName)
                    {                       
                        return (true, GameObjectContainer);
                    }
                }
            }
            return (false, null);
        }
        else if (itemToCheck.gameObject.GetComponent<ItemControl>().itemData.stackable == Items.Stackable.stackableUnlimited)
        {
            for (int a = 0; Slots.Count > a; a++)
            {
                GameObjectContainer = Slots[a].transform.Find("GameObjectContainer").gameObject;
                if (GameObjectContainer.transform.childCount > 0)
                {
                    ItemExistsInContainer = GameObjectContainer.transform.GetChild(0).gameObject;
                    if (ItemExistsInContainer.GetComponent<ItemControl>().itemData.ItameName == itemToCheck.GetComponent<ItemControl>().itemData.ItameName)
                    {
                        return (true, GameObjectContainer);
                    }
                }
            }
            return (false, null);
        }
        else
        {
            return (false, null);
        }
    }
    private (bool inventoryIsFull, GameObject gameObjectContainer) ReturnFirstAvailableSlotInTheInventory()
    {
        for (int a = 0; Slots.Count > a; a++)
        {
            GameObjectContainer = Slots[a].transform.Find("GameObjectContainer").gameObject;
            if (GameObjectContainer.transform.childCount == 0)
            {
                return (false,GameObjectContainer);
            }
        }
        return (true,null);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CheckNumberOfSlots();
        if (collider.gameObject.GetComponent<ItemControl>() != null)
        {
            if (collider.gameObject.GetComponent<ItemControl>().itemData.itemType == Items.ItemType.PickUpAble)
            {
                if (CheckIfItemTypeIsAlreadyEquipped(collider.gameObject).itemEquipped == false)
                {
                    GameObject container = CheckIfItemTypeIsAlreadyEquipped(collider.gameObject).gameObjectContainer;
                    MoveTransformItem(collider.gameObject, container);
                    MoveSprites(collider.gameObject, container);
                }
                else
                {
                    if (CheckIfItemExistInInventoryAndStackable(collider.gameObject).itemExist == true)
                    {
                        GameObject container = CheckIfItemExistInInventoryAndStackable(collider.gameObject).gameObjectContainer;
                        MoveTransformItem(collider.gameObject, container);
                        MoveSprites(collider.gameObject, container);
                        GameObject slot = container.transform.parent.gameObject;
                        UiItemControl uiItemControl = slot.GetComponent<UiItemControl>();

                        uiItemControl.UpdateNuberOfItemInSlot(slot, false);
                    }
                    else
                    {
                        if (ReturnFirstAvailableSlotInTheInventory().inventoryIsFull == false)
                        {
                            GameObject container = ReturnFirstAvailableSlotInTheInventory().gameObjectContainer;
                            MoveTransformItem(collider.gameObject, container);
                            MoveSprites(collider.gameObject, container);
                        }
                        else
                        {
                            Debug.Log("Inventuriy is full");
                        }

                    }

                }


            }
            else if (collider.gameObject.GetComponent<ItemControl>().itemData.itemType == Items.ItemType.PermanentUsage)
            {
                collider.gameObject.GetComponent<ItemControl>().ApplayBuffs();
                Destroy(collider.gameObject);
                Debug.Log("***Permanent Usage item has been applayd.***");

            }

        }
    }



}