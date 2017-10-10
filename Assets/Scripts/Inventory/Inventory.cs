using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public Image[] itemImages = new Image[numItemSlots];
    public Item[] items = new Item[numItemSlots];
    public GameObject[] itemSlots = new GameObject[numItemSlots];

    public const int numItemSlots = 12;

    private GameObject infoInventoryPanel;
    private Text infoItemNameText;
    private Text infoItemDescriptionText;
    private Text infoItemValueText;
    private GameObject infoButtonPanel;
    public Item infoPanelItem;
    public Item itemToMove;
    public Item itemToReplace;
    public Item itemToConsume;
    public GameObject itemSlotToReplace;

    public GameObject invConfirmationPanel;
    public Text invConfirmationText;
    public Button invYesButton;

    public bool usingItem = false;
    public bool movingItem = false;
    public bool droppingItem = false;

    private GameObject player;
    private PlayerStats PS;

    private bool usingConsumable = false;

    public void Awake()
    {
        infoInventoryPanel = GameObject.Find("Inventory Info Panel");
        infoItemNameText = GameObject.Find("InfoItemName").GetComponent<Text>();
        infoItemDescriptionText = GameObject.Find("InfoItemDescription").GetComponent<Text>();
        infoItemValueText = GameObject.Find("InfoItemValue").GetComponent<Text>();
        infoButtonPanel = GameObject.Find("Info Button Panel");
        print(infoPanelItem + " at Start");

        invConfirmationPanel = GameObject.Find("InvConfirmationPanel");
        invConfirmationText = GameObject.Find("InvConfirmationText").GetComponent<Text>();
        invYesButton = GameObject.Find("InvYesButton").GetComponent<Button>();

        infoInventoryPanel.SetActive(false);
        invConfirmationPanel.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        PS = player.GetComponent<PlayerStats>();
    }

    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                itemSlots[i].GetComponent<Slot>().UpdateSlotItems();
                return;
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    { 
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                itemSlots[i].GetComponent<Slot>().UpdateSlotItems();
                EnableInfoPanel();
                return;
            }
        }
    }


    public void EnableInfoPanel(Item itemToDisplay)
    {
        infoInventoryPanel.SetActive(true);
        infoPanelItem = itemToDisplay;
        print(infoPanelItem + " is in the info panel");
        infoItemNameText.text = itemToDisplay.itemName;
        infoItemDescriptionText.text = itemToDisplay.itemDescription;
        infoItemValueText.text = "Value " + itemToDisplay.itemValue.ToString();
        infoButtonPanel.SetActive(true);
    }

    public void EnableInfoPanel()
    {
        infoInventoryPanel.SetActive(true);
        infoItemNameText.text = null;
        infoItemDescriptionText.text = null;
        infoItemValueText.text = null;
        infoPanelItem = null;
        infoButtonPanel.SetActive(false);
    }

    public void DisableInfoPanel()
    {
        infoInventoryPanel.SetActive(false);
        infoItemNameText.text = null;
        infoItemDescriptionText.text = null;
        infoItemValueText.text = null;
        infoPanelItem = null;
        infoButtonPanel.SetActive(false);
    }

    public void MoveItem()
    {
        movingItem = true;
        itemToMove = infoPanelItem;
    }

    public void ChangingItem()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (itemSlots[i] == itemSlotToReplace)
            {
                items[i] = itemToReplace;
                itemImages[i].sprite = itemToReplace.sprite;
                itemImages[i].enabled = true;
                itemSlots[i].GetComponent<Slot>().UpdateSlotItems();
                return;
            }
        }
    }

    public void DropItem() // Used to Activate the Confirmation Panel
    {
        droppingItem = true;
        InvConfirmation();
    }

    public void DroppingItem() // Actual Dropping of item after confirmation
    {
        invConfirmationPanel.SetActive(false);
        RemoveItem(infoPanelItem);
        droppingItem = false;
    }

    public void UseConsumable() // Used to activate the Confirmation Panel
    {
        usingItem = true;
        InvConfirmation();
    }

    public void ConfirmationWait() // Deactivating the Confirmation Panel and waiting
    {
        usingItem = false;
        droppingItem = false;
    }

    public void UsingConsumable()
    {
        invConfirmationPanel.SetActive(false);

        if (infoPanelItem.consumable) // Checks to see if item is a consumable
        {
            itemToConsume = infoPanelItem; // Sets the itemToConsume to the infoPanelItem
            infoPanelItem = null;

            // Adds the items effect to the player stats
            PS.stamina += itemToConsume.stamina;
            PS.agility += itemToConsume.agility;
            PS.currentDefence += itemToConsume.defence;
            PS.playerHealth += itemToConsume.health;

            RemoveItem(itemToConsume); // Removes item 
            usingItem = false;
        }

        else
        {

        }


    }


    public void InvConfirmation()
    {
        invConfirmationPanel.SetActive(true);
        if (droppingItem)
        {
            invConfirmationText.text = "Do you wish to DROP this item?";
            invYesButton.onClick.AddListener(() => DroppingItem());
        }

        else if (usingItem)
        {
            invConfirmationText.text = "Do you wish to USE this item?";
            invYesButton.onClick.AddListener(() => UsingConsumable());
        }

    }



}
 