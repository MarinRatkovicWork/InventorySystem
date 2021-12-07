using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UiItemControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
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
    public TMP_Text ItemManaMaxText;
    public TMP_Text ItemHealthMaxText;
    public TMP_Text ItemReplenishHealthText;
    public TMP_Text ItemReaplenishManaText;

    private GameObject imageItem;
    private Vector2 ItemImagePosition;

    void Start()
    {
        ItemPreviewe.SetActive(false);       
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (DropToWorldPanel != eventData.pointerEnter.gameObject)
        {
            GetItemData(eventData.pointerEnter.transform.gameObject);
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
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
        ItemManaMaxText.text = itemFound.GetComponent<ItemControl>().itemData.PermanentManaIncrease.ToString();
        ItemHealthMaxText.text = itemFound.GetComponent<ItemControl>().itemData.PermanentHelthIncrease.ToString();
        ItemReplenishHealthText.text = itemFound.GetComponent<ItemControl>().itemData.ReplenishHelth.ToString();
        ItemReaplenishManaText.text = itemFound.GetComponent<ItemControl>().itemData.ReplenishMana.ToString();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        imageItem = data.pointerDrag.gameObject.transform.Find("Image").gameObject;
        imageItem.transform.SetParent(GameObject.Find("RightHolder").transform);
        ItemImagePosition = imageItem.transform.position;
        imageItem.transform.localScale = imageItem.transform.localScale * 2;


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
        GameObject CurentSlot = data.pointerDrag.gameObject;
        imageDragd.transform.SetParent(data.pointerDrag.transform);
        imageDragd.transform.SetSiblingIndex(3);


        SwopSlots(data);
    }

    private void SwopSlots(PointerEventData data)
    {

        GameObject CurentSlot = data.pointerDrag.gameObject;
        GameObject GameObjectContainer = CurentSlot.transform.Find("GameObjectContainer").gameObject;
        if (GameObjectContainer.transform.childCount > 0)
        {
            GameObject ItemInContainer = GameObjectContainer.transform.GetChild(0).gameObject;
            GameObject SlotToSwopWith = data.pointerCurrentRaycast.gameObject;

            if (SlotToSwopWith != DropToWorldPanel)
            {
                GameObject GameObjectContainerToSwap = SlotToSwopWith.transform.Find("GameObjectContainer").gameObject;
                if (SlotToSwopWith.transform.parent == CurentSlot.transform.parent)
                {
                    if (GameObjectContainerToSwap.transform.childCount > 0)
                    {



                        List<GameObject> StartItems = new List<GameObject>();

                        for (int i = 0; i < GameObjectContainer.transform.childCount; i++)
                        {
                            StartItems.Add(GameObjectContainer.transform.GetChild(i).gameObject);

                        }

                        List<GameObject> SwapItems = new List<GameObject>();

                        for (int i = 0; i < GameObjectContainerToSwap.transform.childCount; i++)
                        {
                            SwapItems.Add(GameObjectContainerToSwap.transform.GetChild(i).gameObject);
                        }

                        for (int i = 0; i < SwapItems.Count; i++)
                        {
                            SwapItems[i].transform.parent = GameObjectContainer.transform;
                        }

                        for (int i = 0; i < StartItems.Count; i++)
                        {
                            StartItems[i].transform.parent = GameObjectContainerToSwap.transform;

                        }
                        Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                        ItemImage.sprite = GameObjectContainer.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                        UpdateNuberOfItemInSlot(CurentSlot, false);

                        Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                        SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                        UpdateNuberOfItemInSlot(SlotToSwopWith, false);
                    }

                    else
                    {
                        List<GameObject> StartItems = new List<GameObject>();

                        for (int i = 0; i < GameObjectContainer.transform.childCount; i++)
                        {
                            StartItems.Add(GameObjectContainer.transform.GetChild(i).gameObject);
                        }
                        for (int i = 0; i < StartItems.Count; i++)
                        {
                            StartItems[i].transform.parent = GameObjectContainerToSwap.transform;

                        }
                        Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                        ItemImage.sprite = null;
                        UpdateNuberOfItemInSlot(CurentSlot, false);

                        Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                        SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                        UpdateNuberOfItemInSlot(SlotToSwopWith, false);
                    }
                }
                else if (SlotToSwopWith.transform.parent.name == EqupmentSlots.name && ItemInContainer.GetComponent<ItemControl>().itemData.usageType == Items.UsageType.Equpment)
                {
                    if (ItemInContainer.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Hed && SlotToSwopWith.name == "HeadSlot")
                    {
                        GameObject StartItem = GameObjectContainer.transform.GetChild(0).gameObject;
                        if (GameObjectContainerToSwap.transform.childCount > 0)
                        {
                            GameObject SwopItem = GameObjectContainerToSwap.transform.GetChild(0).gameObject;
                            SwopItem.transform.parent = GameObjectContainer.transform;
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = GameObjectContainer.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                            SwopItem.GetComponent<ItemControl>().RemoveBuffs();
                        }
                        else
                        {
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = null;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                        }
                    }
                    else if (ItemInContainer.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Torso && SlotToSwopWith.name == "TorsoSlot")
                    {
                        GameObject StartItem = GameObjectContainer.transform.GetChild(0).gameObject;
                        if (GameObjectContainerToSwap.transform.childCount > 0)
                        {
                            GameObject SwopItem = GameObjectContainerToSwap.transform.GetChild(0).gameObject;
                            SwopItem.transform.parent = GameObjectContainer.transform;
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = GameObjectContainer.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                            SwopItem.GetComponent<ItemControl>().RemoveBuffs();
                        }
                        else
                        {
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = null;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                        }
                    }
                    else if (ItemInContainer.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Boots && SlotToSwopWith.name == "BootsSlot")
                    {
                        GameObject StartItem = GameObjectContainer.transform.GetChild(0).gameObject;
                        if (GameObjectContainerToSwap.transform.childCount > 0)
                        {
                            GameObject SwopItem = GameObjectContainerToSwap.transform.GetChild(0).gameObject;
                            SwopItem.transform.parent = GameObjectContainer.transform;
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = GameObjectContainer.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                            SwopItem.GetComponent<ItemControl>().RemoveBuffs();
                        }
                        else
                        {
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = null;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                        }
                    }
                    else if (ItemInContainer.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Wepon && SlotToSwopWith.name == "WeaponSlot")
                    {
                        GameObject StartItem = GameObjectContainer.transform.GetChild(0).gameObject;
                        if (GameObjectContainerToSwap.transform.childCount > 0)
                        {
                            GameObject SwopItem = GameObjectContainerToSwap.transform.GetChild(0).gameObject;
                            SwopItem.transform.parent = GameObjectContainer.transform;
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = GameObjectContainer.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                            SwopItem.GetComponent<ItemControl>().RemoveBuffs();
                        }
                        else
                        {
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = null;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                        }
                    }
                    else if (ItemInContainer.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Shild && SlotToSwopWith.name == "ShildSlot")
                    {
                        GameObject StartItem = GameObjectContainer.transform.GetChild(0).gameObject;
                        if (GameObjectContainerToSwap.transform.childCount > 0)
                        {
                            GameObject SwopItem = GameObjectContainerToSwap.transform.GetChild(0).gameObject;
                            SwopItem.transform.parent = GameObjectContainer.transform;
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = GameObjectContainer.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                            SwopItem.GetComponent<ItemControl>().RemoveBuffs();
                        }
                        else
                        {
                            StartItem.transform.parent = GameObjectContainerToSwap.transform;
                            Image SwapItemImage = SlotToSwopWith.transform.Find("Image").gameObject.GetComponent<Image>();
                            SwapItemImage.sprite = GameObjectContainerToSwap.transform.GetChild(0).gameObject.GetComponent<ItemControl>().itemData.Artwork;
                            Image ItemImage = CurentSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                            ItemImage.sprite = null;
                            StartItem.GetComponent<ItemControl>().ApplayBuffs();
                        }
                    }
                }
            }
            else
            {
                DropToWorld(CurentSlot);
            }
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            SwopEqupmentOnRightClik(eventData);
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
    }
    private void SwopEqupmentOnRightClik(PointerEventData data)
    {
        GameObject ItemToPlace = data.pointerPress.gameObject;
        GameObject ItemToPlaceIamge = ItemToPlace.transform.Find("Image").gameObject;
        GameObject ItemToPlaceParent = ItemToPlace.transform.parent.gameObject;
        GameObject GameObjectContainerStartSwap = ItemToPlace.transform.Find("GameObjectContainer").gameObject;

        if (GameObjectContainerStartSwap.transform.childCount > 0)
        {
            GameObject ItemInContenerStart = GameObjectContainerStartSwap.transform.GetChild(0).gameObject;
            if (ItemInContenerStart.GetComponent<ItemControl>().itemData.usageType == Items.UsageType.Equpment)
            {
                if (ItemToPlaceParent.name == "EquipmentSlots")
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
                            ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                            ItemToPlaceIamge.GetComponent<Image>().sprite = null;
                            ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                            ItemInContenerStart.GetComponent<ItemControl>().RemoveBuffs();
                            break;
                        }

                    }

                }
                else if (ItemToPlaceParent.name == "ItemSlotContanier")
                {
                    GameObject ItemSlotContanier = EqupmentSlots;
                    List<GameObject> slots = new List<GameObject>();
                    for (int i = 0; i < ItemSlotContanier.transform.childCount; i++)
                    {
                        slots.Add(ItemSlotContanier.transform.GetChild(i).gameObject);
                    }

                    foreach (GameObject slot in slots)
                    {
                        GameObject ItemContainerInSlot = slot.transform.Find("GameObjectContainer").gameObject;
                        if (slot.name == "HeadSlot" && ItemInContenerStart.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Hed)
                        {
                            if (ItemContainerInSlot.transform.childCount == 0)
                            {
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = null;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                break;
                            }
                            else
                            {
                                GameObject ItemInSlot = ItemContainerInSlot.transform.GetChild(0).gameObject;
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = ItemInSlot.GetComponent<ItemControl>().itemData.Artwork;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInSlot.transform.parent = GameObjectContainerStartSwap.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                ItemInSlot.GetComponent<ItemControl>().RemoveBuffs();
                                break;
                            }
                        }
                        if (slot.name == "TorsoSlot" && ItemInContenerStart.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Torso)
                        {
                            if (ItemContainerInSlot.transform.childCount == 0)
                            {
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = null;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                break;
                            }
                            else
                            {
                                GameObject ItemInSlot = ItemContainerInSlot.transform.GetChild(0).gameObject;
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = ItemInSlot.GetComponent<ItemControl>().itemData.Artwork;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInSlot.transform.parent = GameObjectContainerStartSwap.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                ItemInSlot.GetComponent<ItemControl>().RemoveBuffs();
                                break;
                            }
                        }
                        if (slot.name == "BootsSlot" && ItemInContenerStart.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Boots)
                        {
                            if (ItemContainerInSlot.transform.childCount == 0)
                            {
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = null;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                break;
                            }
                            else
                            {
                                GameObject ItemInSlot = ItemContainerInSlot.transform.GetChild(0).gameObject;
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = ItemInSlot.GetComponent<ItemControl>().itemData.Artwork;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInSlot.transform.parent = GameObjectContainerStartSwap.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                ItemInSlot.GetComponent<ItemControl>().RemoveBuffs();
                                break;
                            }
                        }
                        if (slot.name == "WeaponSlot" && ItemInContenerStart.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Wepon)
                        {
                            if (ItemContainerInSlot.transform.childCount == 0)
                            {
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = null;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                break;
                            }
                            else
                            {
                                GameObject ItemInSlot = ItemContainerInSlot.transform.GetChild(0).gameObject;
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = ItemInSlot.GetComponent<ItemControl>().itemData.Artwork;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInSlot.transform.parent = GameObjectContainerStartSwap.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                ItemInSlot.GetComponent<ItemControl>().RemoveBuffs();
                                break;
                            }
                        }
                        if (slot.name == "ShildSlot" && ItemInContenerStart.GetComponent<ItemControl>().itemData.equpment == Items.Equpment.Shild)
                        {
                            if (ItemContainerInSlot.transform.childCount == 0)
                            {
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = null;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                break;
                            }
                            else
                            {
                                GameObject ItemInSlot = ItemContainerInSlot.transform.GetChild(0).gameObject;
                                GameObject ImageSlot = slot.transform.Find("Image").gameObject;
                                ImageSlot.GetComponent<Image>().sprite = ItemInContenerStart.GetComponent<ItemControl>().itemData.Artwork;
                                ItemToPlaceIamge.GetComponent<Image>().sprite = ItemInSlot.GetComponent<ItemControl>().itemData.Artwork;
                                ItemInContenerStart.transform.parent = ItemContainerInSlot.transform;
                                ItemInSlot.transform.parent = GameObjectContainerStartSwap.transform;
                                ItemInContenerStart.GetComponent<ItemControl>().ApplayBuffs();
                                ItemInSlot.GetComponent<ItemControl>().RemoveBuffs();
                                break;
                            }
                        }

                    }
                }







            }




        }


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
                    ItemConsume.GetComponent<ItemControl>().ApplayBuffs();

                    if (GameObjectContainerConsume.transform.childCount == 1)
                    {
                        GameObject Image = ItemToConsumeStart.transform.Find("Image").gameObject;
                        Image.GetComponent<Image>().sprite = null;
                        Destroy(ItemConsume);

                    }
                    else
                    {
                        Destroy(ItemConsume);
                        UpdateNuberOfItemInSlot(ItemToConsumeStart, true);
                    }

                }
            }
        }


    }
    public void UpdateNuberOfItemInSlot(GameObject Slot, bool minus)
    {
        GameObject GameObjectContainerFoItems = Slot.transform.Find("GameObjectContainer").gameObject;
        GameObject textNuberOfItems = Slot.transform.Find("NumberText").gameObject;
        if (minus == true)
        {
            int nuberOfItems = GameObjectContainerFoItems.transform.childCount - 1;
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
            int nuberOfItems = GameObjectContainerFoItems.transform.childCount;
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