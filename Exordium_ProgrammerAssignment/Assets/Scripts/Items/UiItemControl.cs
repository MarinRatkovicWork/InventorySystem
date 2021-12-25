using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class UiItemControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public GameObject ActiveBuffHolder;
    
    private GameObject GameObjectContainer;

    public GameObject ItemPreviewe;
    private GameObject ItemFound;

    public GameObject EqupmentSlots;
    public GameObject Player;
    public GameObject ItemWorldContainer;
    public GameObject DropToWorldPanel;


    public Image Artwork;

    public TMP_Text ItemNameText;
    public TMP_Text ItemAttackText;
    public TMP_Text ItemDefenceText;
    public TMP_Text ItemTypeText;
    public TMP_Text ItemEqupmentText;
    public TMP_Text ItemStrenghtText;
    public TMP_Text ItemDexterityText;
    public TMP_Text ItemAgilityText;
    public TMP_Text ItemIntelligenceText;
    public TMP_Text ItemLuckText;
    public TMP_Text ItemManaMaxText;
    public TMP_Text ItemHealthMaxText;
    public TMP_Text ItemReplenishHealthText;
    public TMP_Text ItemReaplenishManaText;

    public GameObject ItemDurabilityText;
    public GameObject DurabilityBar;

    private GameObject imageItem;
    private Vector2 ItemImagePosition;
    private bool splitPossible;
    public GameObject SplitScreen;

   

    private bool fallowStart;
    private GameObject clickObjectFallow;
    private GameObject clickObjectDrop;
    private GameObject fallowImage;
    private Transform fallowImagePosition;

    private GameObject pointerEnterData;

    void Start()
    {
        ItemPreviewe.SetActive(false);
        SplitScreen.SetActive(false);
    }
    void Update()
    {
        /*
        if(fallowStart == true)
        {
            fallowImage.transform.position = Input.mousePosition;
        
        }*/

        if (pointerEnterData != null) {
            GameObjectContainer = pointerEnterData.transform.Find("GameObjectContainer").gameObject;
            if (GameObjectContainer.transform.childCount > 0)
            {
                ItemFound = GameObjectContainer.transform.GetChild(0).gameObject;
                ItemDurabilityText.SetActive(true);
                ItemDurabilityText.GetComponent<TMP_Text>().text = ItemFound.GetComponent<ItemControl>().currentDurability.ToString();
                DurabilityBar.GetComponent<Slider>().maxValue = ItemFound.GetComponent<ItemControl>().itemData.Durability;
                DurabilityBar.GetComponent<Slider>().value = ItemFound.GetComponent<ItemControl>().currentDurability;
            }

            
        }
        
        
    }

    public bool CheckIfSplitPossible(GameObject slot)
    {
        GameObject gameObjectContainer = slot.transform.Find("GameObjectContainer").gameObject;
        if (gameObjectContainer.transform.childCount > 1)
        {
            return true;
        }
        return false;
    }
    

    public void DisplayWarningSplit(bool diplayWarning)
    {
        GameObject warningSplit = SplitScreen.transform.Find("WarningSplit").gameObject;
        if(diplayWarning == true)
        {
            warningSplit.SetActive(true);
        }
        if (diplayWarning == false)
        {
            warningSplit.SetActive(false);
        }
    }
    public void UpdateSplitValueWithSlider()
    {
        GameObject splitBar = SplitScreen.transform.Find("SplitBar").gameObject;
        GameObject moveStack = SplitScreen.transform.Find("MoveStack").gameObject;
        moveStack.GetComponent<TMP_Text>().text = splitBar.GetComponent<Slider>().value.ToString();
    }
    private void TransferDataToSplitScreen(GameObject slot)
    {
       if( CheckIfSplitPossible(slot) == true)
        {
            DisplayWarningSplit(false);
            GameObject imageHolder = SplitScreen.transform.Find("ImageHolder").gameObject;
         GameObject imageStack = imageHolder.transform.Find("Image").gameObject;
         GameObject imageSlot = slot.transform.Find("Image").gameObject;
         imageStack.GetComponent<Image>().sprite = imageSlot.GetComponent<Image>().sprite;

         GameObject stackMax = SplitScreen.transform.Find("StackMax").gameObject;
         Debug.Log(stackMax.name);
         GameObject gameObjectContainer = slot.transform.Find("GameObjectContainer").gameObject;
         Debug.Log(gameObjectContainer.name);
         stackMax.GetComponent<TMP_Text>().text = gameObjectContainer.transform.childCount.ToString();

         GameObject splitBar = SplitScreen.transform.Find("SplitBar").gameObject;
         splitBar.GetComponent<Slider>().maxValue = gameObjectContainer.transform.childCount;
            SplitScreen.transform.position = slot.transform.position + new Vector3(-60,62,0);
            
        }
        else
        {
            DisplayWarningSplit(true);
         SplitScreen.transform.position = slot.transform.position + new Vector3(-60,62,0);
        }

        

       

    }
    private GameObject StartFallow(GameObject clickObjectFallow )
    {              
        GameObject CurentSlot = clickObjectFallow;

        if (CurentSlot.transform.childCount > 0)
        {
            GameObject GameObjectContainer = CurentSlot.transform.Find("GameObjectContainer").gameObject;
            if (GameObjectContainer.transform.childCount > 0)
            {
                GameObject ImageFallow = clickObjectFallow.gameObject.transform.Find("Image").gameObject;
                fallowImagePosition = ImageFallow.transform;
                ImageFallow.transform.SetParent(GameObject.Find("RightHolder").transform);               
                ImageFallow.transform.localScale = ImageFallow.transform.localScale * 2;               
                fallowStart = true;
                return ImageFallow;
            }
            fallowStart = false;
            return null;
        }
        fallowStart = false;
        return null;
    }   
    private void EndFallow(GameObject clickObjectFallow, GameObject clickObjectDrop, GameObject fallowImage)
    {
        
            GameObject imageItemParent = GameObject.Find("RightHolder").gameObject;
            GameObject imageDragd = imageItemParent.transform.Find("Image").gameObject;
           imageDragd.transform.position = clickObjectFallow.transform.position;
            imageDragd.transform.localScale = imageDragd.transform.localScale / 2;
            imageDragd.transform.SetParent(clickObjectFallow.transform);
            imageDragd.transform.SetSiblingIndex(2);

            GameObject startSwap = clickObjectFallow.gameObject;
            GameObject endSwap = clickObjectDrop.gameObject;
            SwopSlots(startSwap, endSwap);

        clickObjectDrop = null;
        fallowImage = null;
        fallowStart = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject startSwop = eventData.pointerPress.gameObject;
            SwopEqupmentOnRightClik(startSwop);
        }
        else
        if (eventData.button == PointerEventData.InputButton.Middle)
        {
            ConsumeItem(eventData);
        }
        else if (Input.GetKey(KeyCode.Q) && eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Radi");
            
            DropToWorld(eventData.pointerPress.gameObject);

        }
        else if (Input.GetKey(KeyCode.R) && eventData.button == PointerEventData.InputButton.Left)
        {
            TransferDataToSplitScreen(eventData.pointerPress.gameObject);
            SplitScreen.SetActive(true);

        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            
            /*
            if (fallowStart == false)
            {
                Debug.Log("100");
                clickObjectFallow = eventData.pointerPress.gameObject;
                fallowImage = StartFallow(clickObjectFallow);
            }
            else if (fallowStart == true)
            {
                Debug.Log("200");
                clickObjectDrop = eventData.pointerPress.gameObject;
                EndFallow(clickObjectFallow, clickObjectDrop, imageItem);
            }*/

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (DropToWorldPanel != eventData.pointerEnter.gameObject)
        {
            pointerEnterData = eventData.pointerEnter.gameObject;
            GetItemData(eventData.pointerEnter.transform.gameObject);
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEnterData = null;
        ItemPreviewe.SetActive(false);
    }
    private void GetItemData(GameObject Item)
    {
        GameObjectContainer = Item.transform.Find("GameObjectContainer").gameObject;
        if (GameObjectContainer.transform.childCount > 0)
        {
            ItemFound = GameObjectContainer.transform.GetChild(0).gameObject;
            TransferDataToItemPreview(ItemFound);
            ItemPreviewe.SetActive(true);
        }
    }

    private void TransferDataToItemPreview(GameObject itemFound)
    {

        Artwork.sprite = itemFound.GetComponent<ItemControl>().itemData.Artwork;
        ItemNameText.text = itemFound.GetComponent<ItemControl>().itemData.ItameName;
        ItemAttackText.text = itemFound.GetComponent<ItemControl>().itemData.Attack.ToString();
        ItemDefenceText.text = itemFound.GetComponent<ItemControl>().itemData.Defence.ToString();
        ItemTypeText.text = itemFound.GetComponent<ItemControl>().itemData.usageType.ToString();
        ItemEqupmentText.text = itemFound.GetComponent<ItemControl>().itemData.equpment.ToString();
        ItemStrenghtText.text = itemFound.GetComponent<ItemControl>().itemData.Strenght.ToString();
        ItemDexterityText.text = itemFound.GetComponent<ItemControl>().itemData.Dexterity.ToString();
        ItemAgilityText.text = itemFound.GetComponent<ItemControl>().itemData.Agility.ToString();
        ItemIntelligenceText.text = itemFound.GetComponent<ItemControl>().itemData.Intelligence.ToString();
        ItemLuckText.text = itemFound.GetComponent<ItemControl>().itemData.Luck.ToString();
        ItemManaMaxText.text = itemFound.GetComponent<ItemControl>().itemData.PermanentManaIncrease.ToString();
        ItemHealthMaxText.text = itemFound.GetComponent<ItemControl>().itemData.PermanentHelthIncrease.ToString();
        ItemReplenishHealthText.text = itemFound.GetComponent<ItemControl>().itemData.ReplenishHelth.ToString();
        ItemReaplenishManaText.text = itemFound.GetComponent<ItemControl>().itemData.ReplenishMana.ToString();
         ItemDurabilityText.SetActive(true);
         ItemDurabilityText.GetComponent<TMP_Text>().text = itemFound.GetComponent<ItemControl>().currentDurability.ToString() ;         
         DurabilityBar.GetComponent<Slider>().maxValue = itemFound.GetComponent<ItemControl>().itemData.Durability;
         DurabilityBar.GetComponent<Slider>().value = itemFound.GetComponent<ItemControl>().currentDurability;

}

    public void OnBeginDrag(PointerEventData data)
    {
        
        GameObject CurentSlot = data.pointerDrag.gameObject;
       
        if (CurentSlot.transform.childCount >0)
        { 
            GameObject GameObjectContainer = CurentSlot.transform.Find("GameObjectContainer").gameObject;
            if (GameObjectContainer.transform.childCount > 0)
            {
                imageItem = data.pointerDrag.gameObject.transform.Find("Image").gameObject;
                imageItem.transform.SetParent(GameObject.Find("RightHolder").transform);
                ItemImagePosition = imageItem.transform.position;
                imageItem.transform.localScale = imageItem.transform.localScale * 2;
            }
            else
            {
                data.pointerDrag = null;
            }
        }
        else
        {
            data.pointerDrag = null;
        }

    }

    public void OnEndDrag(PointerEventData data)
    {
        imageItem.transform.position = ItemImagePosition;
        imageItem.transform.localScale = imageItem.transform.localScale / 2;
        imageItem.transform.SetParent(data.pointerDrag.transform);
        imageItem.transform.SetSiblingIndex(2);
    }
    public void OnDrag(PointerEventData data)
    {       
            imageItem.transform.position = Input.mousePosition;
       
    }
    public void OnDrop(PointerEventData data)
    {
        GameObject imageItemParent = GameObject.Find("RightHolder").gameObject;
        GameObject imageDragd = imageItemParent.transform.Find("Image").gameObject;
        imageDragd.transform.SetParent(data.pointerDrag.transform);
        imageDragd.transform.SetSiblingIndex(3);

        GameObject startSwap = data.pointerDrag.gameObject; 
        GameObject endSwap = data.pointerCurrentRaycast.gameObject;
        SwopSlots(startSwap,endSwap);
    }
     private List<string> ReturnSlotName(Items.Equpment itemsEqupment)
    {
       List<string> slotNames = new List<string>();
       string slotName;
        
        if (itemsEqupment == Items.Equpment.Hed)
        {
            slotName = "HeadSlot";
            slotNames.Add(slotName);

        }
        else if (itemsEqupment == Items.Equpment.Torso)
        {
            slotName = "TorsoSlot";
            slotNames.Add(slotName);
        }
        else if (itemsEqupment == Items.Equpment.Boots)
        {
            slotName = "BootsSlot";
            slotNames.Add(slotName);
        }
        else if (itemsEqupment == Items.Equpment.Shild)
        {
            slotName = "ShildSlot";
            slotNames.Add(slotName);
        }
        else if (itemsEqupment == Items.Equpment.Wepon)
        {
            slotName = "WeaponSlot";
            slotNames.Add(slotName);
        }
        else if (itemsEqupment == Items.Equpment.Ring)
        {
            slotName = "RightRingSlot";
            slotNames.Add(slotName);
            slotName = "LeftRingSlot";
            slotNames.Add(slotName);
        }
        else
        {
            slotName = "";
            slotNames.Add(slotName);
        }       
        return slotNames;
    }
    private void SwapItemsSlotToEqupment(GameObject startSwap, GameObject endSwap)
    {
        GameObject gameObjectContainerStart = startSwap.transform.Find("GameObjectContainer").gameObject;        
        GameObject gameObjectContainerEnd = endSwap.transform.Find("GameObjectContainer").gameObject;

        if (gameObjectContainerStart.transform.childCount > 0)
        {
            GameObject itemStart = gameObjectContainerStart.transform.GetChild(0).gameObject;
            List<string> slotNams = ReturnSlotName(itemStart.GetComponent<ItemControl>().itemData.equpment);
            if (endSwap.transform.parent.name == "EquipmentSlots")
            {
                for (int i = 0; i < slotNams.Count; i++)
                {

                    if (slotNams[i] == endSwap.name)
                    {
                        if (gameObjectContainerEnd.transform.childCount > 0)
                        {
                            GameObject itemEnd = gameObjectContainerEnd.transform.GetChild(0).gameObject;
                            itemEnd.transform.parent = gameObjectContainerStart.transform;
                            itemStart.transform.parent = gameObjectContainerEnd.transform;
                            SwapSpritesSlotToSlot(startSwap, endSwap);
                            itemStart.GetComponent<ItemControl>().ApplayBuffs();
                            itemEnd.GetComponent<ItemControl>().RemoveBuffs();
                        }
                        else
                        {

                            itemStart.transform.parent = gameObjectContainerEnd.transform;
                            SwapSpritesSlotToSlot(startSwap, endSwap);
                            itemStart.GetComponent<ItemControl>().ApplayBuffs();

                        }
                    }
                }
            }
            else if (endSwap.transform.parent.name == "ItemSlotContanier")
            {
                if (gameObjectContainerEnd.transform.childCount > 0)
                {
                    GameObject itemEnd = gameObjectContainerEnd.transform.GetChild(0).gameObject;
                    if (itemEnd.GetComponent<ItemControl>().itemData.equpment == itemStart.GetComponent<ItemControl>().itemData.equpment)
                    {
                        itemEnd.transform.parent = gameObjectContainerStart.transform;
                        itemStart.transform.parent = gameObjectContainerEnd.transform;
                        SwapSpritesSlotToSlot(startSwap, endSwap);
                        itemStart.GetComponent<ItemControl>().ApplayBuffs();
                        itemEnd.GetComponent<ItemControl>().RemoveBuffs();

                    }
                }
                else
                {

                    itemStart.transform.parent = gameObjectContainerEnd.transform;
                    SwapSpritesSlotToSlot(startSwap, endSwap);
                    itemStart.GetComponent<ItemControl>().RemoveBuffs();

                }
            }
            else
            {
                return;
            }
        }
    }
    private void SwapItemItemsSlotToSlot(GameObject startSwap, GameObject endSwap)
    {
        GameObject gameObjectContainerStart = startSwap.transform.Find("GameObjectContainer").gameObject;       
        List<GameObject> startSwapItems = new List<GameObject>();

        for (int i = 0; i < gameObjectContainerStart.transform.childCount; i++)
        {
            startSwapItems.Add(gameObjectContainerStart.transform.GetChild(i).gameObject);
        }
        
        GameObject gameObjectContainerEnd = endSwap.transform.Find("GameObjectContainer").gameObject;     
        List<GameObject> endSwapItems = new List<GameObject>();

        for (int i = 0; i < gameObjectContainerEnd.transform.childCount; i++)
        {
            endSwapItems.Add(gameObjectContainerEnd.transform.GetChild(i).gameObject);
        }            

        foreach(GameObject item in startSwapItems)
        {
            item.transform.parent = gameObjectContainerEnd.transform;
        }
        
        foreach(GameObject item in endSwapItems)
        {
            item.transform.parent = gameObjectContainerStart.transform;
        }
    }

    private void SwapSpritesSlotToSlot(GameObject startSwap, GameObject endSwap)
    {
        GameObject gameObjectContainerStart = startSwap.transform.Find("GameObjectContainer").gameObject;
        Image ItemImage = startSwap.transform.Find("Image").gameObject.GetComponent<Image>();
        if (gameObjectContainerStart.transform.childCount != 0)
        {
            ItemImage.sprite = gameObjectContainerStart.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
            UpdateNuberOfItemInSlot(startSwap, false);
        }
        else
        {
            ItemImage.sprite = null;
            UpdateNuberOfItemInSlot(startSwap, false);
        }

        GameObject gameObjectContainerEnd = endSwap.transform.Find("GameObjectContainer").gameObject;
        Image SwapItemImage = endSwap.transform.Find("Image").gameObject.GetComponent<Image>();
        if (gameObjectContainerEnd.transform.childCount != 0)
        {
            SwapItemImage.sprite = gameObjectContainerEnd.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
            UpdateNuberOfItemInSlot(endSwap, false);
        }
        else
        {
            ItemImage.sprite = null;
            UpdateNuberOfItemInSlot(endSwap, false);
        }
    }

    private void ItemSwapDropPoint(GameObject startSwap, GameObject endSwap)
    {
        if (startSwap.transform.parent == endSwap.transform.parent)
        {
            SwapItemItemsSlotToSlot(startSwap, endSwap);
            SwapSpritesSlotToSlot(startSwap, endSwap);
         
        }
        else if (endSwap.transform.parent.name == EqupmentSlots.name || startSwap.transform.parent.name == EqupmentSlots.name)
        {
            SwapItemsSlotToEqupment(startSwap, endSwap);           

        }
        else
        {
          DropToWorld(startSwap);
          
        }
    }
    private void SwopSlots(GameObject startSwap, GameObject endSwap)
    {      
        GameObject GameObjectContainer = startSwap.transform.Find("GameObjectContainer").gameObject;
        if (GameObjectContainer.transform.childCount > 0)
        {          
            ItemSwapDropPoint(startSwap, endSwap);
        }
    }
    public void DropToWorld(GameObject Slot)

    {
        GameObject GameObjectContainer = Slot.transform.Find("GameObjectContainer").gameObject;
        List<GameObject> Items = new List<GameObject>();
        if (GameObjectContainer.transform.childCount > 1)
        {
            for (int i = 0; i < GameObjectContainer.transform.childCount; i++)
            {
                Items.Add(GameObjectContainer.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].transform.parent = ItemWorldContainer.transform;
                Items[i].SetActive(true);
                Items[i].transform.position = Player.transform.position + (new Vector3(Random.Range(-1, 2), -1, 0) / 3.0F);
            }
            Image ItemImage = Slot.transform.Find("Image").gameObject.GetComponent<Image>();
            ItemImage.sprite = null;

            UpdateNuberOfItemInSlot(Slot, false);

        }
        else
        {
            GameObject ItemToDropToWorld = GameObjectContainer.transform.GetChild(0).gameObject;
            ItemToDropToWorld.transform.SetParent(ItemWorldContainer.transform);
            ItemToDropToWorld.SetActive(true);
            ItemToDropToWorld.transform.position = Player.transform.position + (new Vector3(0, -1, 0) / 3.0F);
            Image ItemImage = Slot.transform.Find("Image").gameObject.GetComponent<Image>();
            ItemImage.sprite = null;

        }


    }    
    private void SwopEqupmentOnRightClik(GameObject startSwop
        )
    {
        GameObject startSwopImage = startSwop.transform.Find("Image").gameObject;
        GameObject startSwopParent = startSwop.transform.parent.gameObject;
        GameObject startSwopContainer = startSwop.transform.Find("GameObjectContainer").gameObject;

        if (startSwopContainer.transform.childCount > 0)
        {
            GameObject startSwopItem = startSwopContainer.transform.GetChild(0).gameObject;
            if (startSwopItem.GetComponent<ItemControl>().itemData.usageType == Items.UsageType.Equpment)
            {
                if (startSwopParent.name == "EquipmentSlots")
                {
                    GameObject ItemSlotContanier = GameObject.Find("ItemSlotContanier").gameObject;
                    List<GameObject> slots = new List<GameObject>();
                    for (int i = 0; i < ItemSlotContanier.transform.childCount; i++)
                    {
                        slots.Add(ItemSlotContanier.transform.GetChild(i).gameObject);
                    }

                    foreach (GameObject slot in slots)
                    {
                        GameObject ItemContainerInSlot = slot.transform.Find("GameObjectContainer").gameObject;
                        if (ItemContainerInSlot.transform.childCount == 0)
                        {
                            GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                            ImageSlot.GetComponent<Image>().sprite = startSwopItem.GetComponent<ItemControl>().itemData.Artwork;
                            startSwopImage.GetComponent<Image>().sprite = null;
                            startSwopItem.transform.parent = ItemContainerInSlot.transform;
                            startSwopItem.GetComponent<ItemControl>().RemoveBuffs();
                            break;
                        }

                    }

                }
                else if (startSwopParent.name == "ItemSlotContanier")
                {
                    GameObject equpmentSlotHolder = EqupmentSlots;
                    List<GameObject> slots = new List<GameObject>();
                    List<string> slotNams = ReturnSlotName(startSwopItem.GetComponent<ItemControl>().itemData.equpment);
                    for (int i = 0; i < equpmentSlotHolder.transform.childCount; i++)
                    {
                        slots.Add(equpmentSlotHolder.transform.GetChild(i).gameObject);
                    }

                    for (int i=0;i<slots.Count;i++)
                    {
                        for (int a = 0; a < slotNams.Count; a++)
                        {
                            GameObject ItemContainerInSlot = slots[i].transform.Find("GameObjectContainer").gameObject;
                            if (slots[i].name == slotNams[a])
                            {
                                if (ItemContainerInSlot.transform.childCount == 0)
                                {
                                    GameObject ImageSlot = slots[i].transform.Find("Image").gameObject;
                                    ImageSlot.GetComponent<Image>().sprite = startSwopItem.GetComponent<ItemControl>().itemData.Artwork;
                                    startSwopImage.GetComponent<Image>().sprite = null;
                                    startSwopItem.transform.parent = ItemContainerInSlot.transform;
                                    startSwopItem.GetComponent<ItemControl>().ApplayBuffs();
                                    goto exit;
                                    
                                }
                                else
                                {
                                    GameObject ItemInSlot = ItemContainerInSlot.transform.GetChild(0).gameObject;
                                    GameObject ImageSlot = slots[i].transform.Find("Image").gameObject;
                                    ImageSlot.GetComponent<Image>().sprite = startSwopItem.GetComponent<ItemControl>().itemData.Artwork;
                                    startSwopImage.GetComponent<Image>().sprite = ItemInSlot.GetComponent<ItemControl>().itemData.Artwork;
                                    startSwopItem.transform.parent = ItemContainerInSlot.transform;
                                    ItemInSlot.transform.parent = startSwopContainer.transform;
                                    startSwopItem.GetComponent<ItemControl>().ApplayBuffs();
                                    ItemInSlot.GetComponent<ItemControl>().RemoveBuffs();
                                    goto exit; 
                                }                              
                           }
                        }
                    }
                }
            }
        }
    exit:;
    }

    private void ConsumeItem(PointerEventData data)
    {
        GameObject ItemToConsumeStart = data.pointerPress.gameObject;
        GameObject ItemToConsumeParent = ItemToConsumeStart.transform.parent.gameObject;
        GameObject GameObjectContainerConsume = ItemToConsumeStart.transform.Find("GameObjectContainer").gameObject;
        if (ItemToConsumeParent.name == "ItemSlotContanier")
        {
            if (GameObjectContainerConsume.transform.childCount > 0)
            {
                GameObject ItemConsume = GameObjectContainerConsume.transform.GetChild(0).gameObject;
                if (ItemConsume.GetComponent<ItemControl>().itemData.usageType == Items.UsageType.Consumable)
                {
                    List<GameObject> BuffSlots = new List<GameObject>();
                    for(int i = 0; i < ActiveBuffHolder.transform.childCount; i++)
                    {
                        BuffSlots.Add(ActiveBuffHolder.transform.GetChild(i).gameObject);
                    }

                    foreach(GameObject slot in BuffSlots)
                    {
                        if(slot.transform.childCount == 0)
                        {
                            if (ItemConsume.GetComponent<ItemControl>().itemData.consumptionType == Items.ConsumptionType.AddBonusesDirectly)
                            {
                                ItemConsume.GetComponent<ItemControl>().ApplayBuffs();
                                if (GameObjectContainerConsume.transform.childCount == 0)
                                {
                                    GameObject Image = ItemToConsumeStart.transform.Find("Image").gameObject;
                                    Image.GetComponent<Image>().sprite = null;
                                }
                                break;
                            }
                            else
                            {
                                ItemConsume.transform.parent = slot.transform;
                                UpdateNuberOfItemInSlot(ItemToConsumeStart, true);
                                if (GameObjectContainerConsume.transform.childCount == 0)
                                {
                                    GameObject Image = ItemToConsumeStart.transform.Find("Image").gameObject;
                                    Image.GetComponent<Image>().sprite = null;
                                }
                                break;
                            }
                        }
                    }
                  

                }
            }
        }


    }
    public void UpdateNuberOfItemInSlot(GameObject Slot, bool minus)
    {
        GameObject GameObjectContainerForItems = Slot.transform.Find("GameObjectContainer").gameObject;
        if (GameObjectContainerForItems.transform.parent.name == "ItemSlotContanier")
        {
            GameObject textNuberOfItems = Slot.transform.Find("NumberText").gameObject;
            if (minus == true)
            {
                int nuberOfItems = GameObjectContainerForItems.transform.childCount - 1;
                if (nuberOfItems > 1)
                {
                    textNuberOfItems.GetComponent<TMP_Text>().SetText(nuberOfItems.ToString());
                }
                else
                {
                    textNuberOfItems.GetComponent<TMP_Text>().SetText("");
                }
            }
            else
            {
                int nuberOfItems = GameObjectContainerForItems.transform.childCount;
                if (nuberOfItems > 1)
                {
                    textNuberOfItems.GetComponent<TMP_Text>().SetText(nuberOfItems.ToString());

                }
                else
                {
                    textNuberOfItems.GetComponent<TMP_Text>().SetText("");
                }
            }
        }
    }

   /* public void ChangeColorItemStacableLimited(GameObject Slot)
    {
        GameObject GameObjectContainerFoItems = Slot.transform.Find("GameObjectContainer").gameObject;

        if (GameObjectContainerFoItems.transform.childCount > 0)
        {
            GameObject itemTaype = GameObjectContainer.transform.GetChild(0).gameObject;
            if (itemTaype.AddComponent<ItemControl>().itemData.stackable == Items.Stackable.stackableLimited)
            {
                GameObject Background = Slot.transform.Find("Background").gameObject;
                int nuberOfItems = GameObjectContainerFoItems.transform.childCount;
                if (nuberOfItems == 1)
                {
                    Background.GetComponent<Image>().color = new Color(142, 224, 142, 255);
                }
                else if (nuberOfItems == 2)
                {
                    Background.GetComponent<Image>().color = new Color(223, 231, 89, 255);
                }
                else if (nuberOfItems == 3)
                {
                    Background.GetComponent<Image>().color = new Color(236, 145, 37, 255);
                }
                else if (nuberOfItems == 4)
                {
                    Background.GetComponent<Image>().color = new Color(255, 67, 72, 255);
                }
            }
        }
        else
        {
            GameObject Background = Slot.transform.Find("Background").gameObject;
            Background.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }*/
}