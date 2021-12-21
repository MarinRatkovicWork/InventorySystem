using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    // movment
    public float moveSpeed = 1.4f;
    public Rigidbody2D Rb;
    public Animator animator;
    Vector2 movement;
    //Item pic up
    public GameObject ItemSlotContanier;
    List<GameObject> Slots = new List<GameObject>();
    private GameObject imageContainer;
    private GameObject GameObjectContainer;
    private GameObject ItemExistsInContainer;
    private GameObject NumberText;
    private bool ItemHasntBeanFaund;
    public GameObject EquipmentSlots;
    // schorcuts
    public GameObject InventoryButoon;
    public GameObject EquipmentButoon;
    public GameObject AttributesButoon;
    public GameObject InventoryPanel;
    public GameObject EquipmentPanel;
    public GameObject AttributesPanel;
    //ItemSpowner
    public List<Items> items = new List<Items>();
    public GameObject item;

    //


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
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);
        Schorcats();
    }

    void FixedUpdate()
    {
        Rb.MovePosition(Rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Schorcats()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpownItems();
        } else

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
        } else
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
        } else
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

        if (collider.gameObject.GetComponent<ItemControl>().itemData.itemType == Items.ItemType.PickUpAble)
        {
          if(  CheckIfItemTypeIsAlreadyEquipped(collider.gameObject).itemEquipped == false)
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


    public void SpownItems()
    {
        
        for (int i = 0; i < items.Count; i++)
        {
            Instantiate(item, new Vector3(Random.Range(-32, 32)/ 5.0F,Random.Range(23, -23) / 5.0F, Random.Range(-1, 0) / 5.0F), Quaternion.identity);
            item.GetComponent < ItemControl >().itemData =items[i];

           
           

        }

    }


    

}