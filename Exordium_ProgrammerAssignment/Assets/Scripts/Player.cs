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
    public List<Items> items = new List<Items> ();
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
        }else

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
        }else
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


    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        for (int i = 0; i < ItemSlotContanier.transform.childCount; i++)
        {
            Slots.Add(ItemSlotContanier.transform.GetChild(i).gameObject);
        }

        bool Equip = false;

        if (collider.gameObject.GetComponent<ItemControl>().itemData.itemType == Items.ItemType.PickUpAble &&
            collider.gameObject.GetComponent<ItemControl>().itemData.usageType == Items.UsageType.Equpment && Equip == false)
        {          
                List<GameObject> EquipmentSlot = new List<GameObject>();

                for (int a = 0; a < EquipmentSlots.transform.childCount; a++)
                {
                    EquipmentSlot.Add(EquipmentSlots.transform.GetChild(a).gameObject);
                }
            if (collider.gameObject.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Hed)
            {
                for (int a = 0; a < EquipmentSlot.Count; a++)
                {

                    if (EquipmentSlot[a].name == "HeadSlot")
                    {
                        GameObject GameObjectContainerEquipment = EquipmentSlot[a].transform.Find("GameObjectContainer").gameObject;
                        if (GameObjectContainerEquipment.transform.childCount == 0)
                        {
                            collider.gameObject.transform.parent = GameObjectContainerEquipment.transform;
                            collider.gameObject.transform.position = GameObjectContainerEquipment.transform.position;
                            GameObject ImageInEqupment = EquipmentSlot[a].transform.Find("Image").gameObject;
                            ImageInEqupment.GetComponent<Image>().sprite = collider.gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            collider.gameObject.GetComponent<ItemControl>().ApplayBuffs();
                            collider.gameObject.SetActive(false);
                            Equip = true;
                            break;                         
                        }
                        else
                        {
                            Equip = false;                            
                            continue;

                        }

                    }
                }
            }
            if (collider.gameObject.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Torso)
            {
                for (int a = 0; a < EquipmentSlot.Count; a++)
                {

                    if (EquipmentSlot[a].name == "TorsoSlot")
                    {
                        GameObject GameObjectContainerEquipment = EquipmentSlot[a].transform.Find("GameObjectContainer").gameObject;
                        if (GameObjectContainerEquipment.transform.childCount == 0)
                        {
                            collider.gameObject.transform.parent = GameObjectContainerEquipment.transform;
                            collider.gameObject.transform.position = GameObjectContainerEquipment.transform.position;
                            GameObject ImageInEqupment = EquipmentSlot[a].transform.Find("Image").gameObject;
                            ImageInEqupment.GetComponent<Image>().sprite = collider.gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            collider.gameObject.GetComponent<ItemControl>().ApplayBuffs();
                            collider.gameObject.SetActive(false);
                            Equip = true;
                            break;
                        }
                        else
                        {
                            Equip = false;
                            continue;

                        }

                    }
                }
            }
            if (collider.gameObject.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Wepon)
            {
                for (int a = 0; a < EquipmentSlot.Count; a++)
                {

                    if (EquipmentSlot[a].name == "WeaponSlot")
                    {
                        GameObject GameObjectContainerEquipment = EquipmentSlot[a].transform.Find("GameObjectContainer").gameObject;
                        if (GameObjectContainerEquipment.transform.childCount == 0)
                        {
                            collider.gameObject.transform.parent = GameObjectContainerEquipment.transform;
                            collider.gameObject.transform.position = GameObjectContainerEquipment.transform.position;
                            GameObject ImageInEqupment = EquipmentSlot[a].transform.Find("Image").gameObject;
                            ImageInEqupment.GetComponent<Image>().sprite = collider.gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            collider.gameObject.GetComponent<ItemControl>().ApplayBuffs();
                            collider.gameObject.SetActive(false);
                            Equip = true;
                            break;
                        }
                        else
                        {
                            Equip = false;
                            continue;

                        }

                    }
                }
            }
            if (collider.gameObject.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Boots)
            {
                for (int a = 0; a < EquipmentSlot.Count; a++)
                {
                  
                    if (EquipmentSlot[a].name == "BootsSlot")
                    {
                        GameObject GameObjectContainerEquipment = EquipmentSlot[a].transform.Find("GameObjectContainer").gameObject;
                        if (GameObjectContainerEquipment.transform.childCount == 0)
                        {
                            collider.gameObject.transform.parent = GameObjectContainerEquipment.transform;
                            collider.gameObject.transform.position = GameObjectContainerEquipment.transform.position;
                            GameObject ImageInEqupment = EquipmentSlot[a].transform.Find("Image").gameObject;
                            ImageInEqupment.GetComponent<Image>().sprite = collider.gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            collider.gameObject.GetComponent<ItemControl>().ApplayBuffs();
                            collider.gameObject.SetActive(false);
                            Equip = true;
                            break;
                        }
                        else
                        {
                            Equip = false;
                            continue;

                        }

                    }
                }
            }
            if (collider.gameObject.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Shild)
            {
                for (int a = 0; a < EquipmentSlot.Count; a++)
                {
                    if (EquipmentSlot[a].name == "ShildSlot")
                    {
                        GameObject GameObjectContainerEquipment = EquipmentSlot[a].transform.Find("GameObjectContainer").gameObject;
                        if (GameObjectContainerEquipment.transform.childCount == 0)
                        {
                            collider.gameObject.transform.parent = GameObjectContainerEquipment.transform;
                            collider.gameObject.transform.position = GameObjectContainerEquipment.transform.position;
                            GameObject ImageInEqupment = EquipmentSlot[a].transform.Find("Image").gameObject;
                            ImageInEqupment.GetComponent<Image>().sprite = collider.gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            collider.gameObject.GetComponent<ItemControl>().ApplayBuffs();
                            collider.gameObject.SetActive(false);
                            Equip = true;
                            break;
                        }
                        else
                        {
                            Equip = false;
                            continue;

                        }

                    }
                }
            }


        }

        if (collider.gameObject.GetComponent<ItemControl>().itemData.itemType == Items.ItemType.PickUpAble && Equip ==false)
        {

            ItemHasntBeanFaund = false;
            for (int i = 0; Slots.Count > i; i++)
            {
                if (collider.gameObject.GetComponent<ItemControl>().itemData.itemType == Items.ItemType.PickUpAble)
                {
                    
                    if (collider.gameObject.GetComponent<ItemControl>().itemData.stackable == Items.Stackable.stackableUnlimited && ItemHasntBeanFaund == false)
                    {
                        for (int a = 0; Slots.Count > a; a++)
                        {
                            GameObjectContainer = Slots[a].transform.Find("GameObjectContainer").gameObject;
                            if (GameObjectContainer.transform.childCount > 0)
                            {

                                ItemExistsInContainer = GameObjectContainer.transform.GetChild(0).gameObject;
                                if (ItemExistsInContainer.GetComponent<ItemControl>().itemData.ItameName == collider.gameObject.GetComponent<ItemControl>().itemData.ItameName)
                                {

                                    collider.gameObject.transform.parent = GameObjectContainer.transform;
                                    collider.gameObject.transform.position = GameObjectContainer.transform.position;
                                    collider.gameObject.SetActive(false);


                                    NumberText = Slots[a].transform.Find("NumberText").gameObject;
                                    NumberText.GetComponent<TMP_Text>().text = GameObjectContainer.transform.childCount.ToString();

                                    return;
                                }
                            }
                        }
                        ItemHasntBeanFaund = true;

                    }

                    if (collider.gameObject.GetComponent<ItemControl>().itemData.stackable == Items.Stackable.stackableUnlimited && ItemHasntBeanFaund == true)
                    {
                        GameObjectContainer = Slots[i].transform.Find("GameObjectContainer").gameObject;
                        if (GameObjectContainer.transform.childCount == 0)
                        {
                            imageContainer = Slots[i].transform.Find("Image").gameObject;
                            imageContainer.gameObject.GetComponent<Image>().sprite = collider.gameObject.GetComponent<SpriteRenderer>().sprite;

                            collider.gameObject.transform.parent = GameObjectContainer.transform;
                            collider.gameObject.transform.position = GameObjectContainer.transform.position;
                            collider.gameObject.SetActive(false);
                            ItemHasntBeanFaund = false;
                            
                            return;
                        }
                    }

                    if (collider.gameObject.GetComponent<ItemControl>().itemData.stackable == Items.Stackable.stackableLimited && ItemHasntBeanFaund == false)
                    {
                        for (int a = 0; Slots.Count > a; a++)
                        {
                            GameObjectContainer = Slots[a].transform.Find("GameObjectContainer").gameObject;

                            if (GameObjectContainer.transform.childCount < 4 && GameObjectContainer.transform.childCount > 0)
                            {

                                ItemExistsInContainer = GameObjectContainer.transform.GetChild(0).gameObject;
                                if (ItemExistsInContainer.GetComponent<ItemControl>().itemData.ItameName == collider.gameObject.GetComponent<ItemControl>().itemData.ItameName)
                                {

                                    collider.gameObject.transform.parent = GameObjectContainer.transform;
                                    collider.gameObject.transform.position = GameObjectContainer.transform.position;
                                    collider.gameObject.SetActive(false);

                                    NumberText = Slots[a].transform.Find("NumberText").gameObject;
                                    NumberText.GetComponent<TMP_Text>().text = GameObjectContainer.transform.childCount.ToString();

                                    return;
                                }

                            }



                        }
                        ItemHasntBeanFaund = true;

                    }

                    if (collider.gameObject.GetComponent<ItemControl>().itemData.stackable == Items.Stackable.stackableLimited && ItemHasntBeanFaund == true)
                    {
                        GameObjectContainer = Slots[i].transform.Find("GameObjectContainer").gameObject;
                        if (GameObjectContainer.transform.childCount == 0)
                        {

                            imageContainer = Slots[i].transform.Find("Image").gameObject;
                            imageContainer.gameObject.GetComponent<Image>().sprite = collider.gameObject.GetComponent<SpriteRenderer>().sprite;

                            collider.gameObject.transform.parent = GameObjectContainer.transform;
                            collider.gameObject.transform.position = GameObjectContainer.transform.position;
                            collider.gameObject.SetActive(false);
                            ItemHasntBeanFaund = false;
                            
                            return;
                        }
                    }

                    if (collider.gameObject.GetComponent<ItemControl>().itemData.stackable == Items.Stackable.UnStackable)
                    {
                        GameObjectContainer = Slots[i].transform.Find("GameObjectContainer").gameObject;
                        if (GameObjectContainer.transform.childCount == 0)
                        {

                            imageContainer = Slots[i].transform.Find("Image").gameObject;
                            imageContainer.gameObject.GetComponent<Image>().sprite = collider.gameObject.GetComponent<SpriteRenderer>().sprite;

                            collider.gameObject.transform.parent = GameObjectContainer.transform;
                            collider.gameObject.transform.position = GameObjectContainer.transform.position;
                            collider.gameObject.SetActive(false);
                            ItemHasntBeanFaund = false;
                        
                            return;
                        }
                    }
                }               
            }
        }
        else if (collider.gameObject.GetComponent<ItemControl>().itemData.itemType == Items.ItemType.PermanentUsage)
        {
            collider.gameObject.GetComponent<ItemControl>().ApplayBuffs();
            Destroy(collider.gameObject);

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