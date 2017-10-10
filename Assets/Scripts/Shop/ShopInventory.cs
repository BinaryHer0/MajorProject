using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour
{

    [Header("Arrays")]
    public Image[] shopItemImages = new Image[numShopItemSlots];
    public Item[] shopItems = new Item[numShopItemSlots];
    public GameObject[] shopItemSlots = new GameObject[numShopItemSlots];
    public Item[] startItems = new Item[numShopItemSlots];

    public const int numShopItemSlots = 24;

    [Header("Info Panel")]
    public GameObject infoInventoryPanel;
    private Text infoItemNameText;
    private Text infoItemDescriptionText;
    private Text infoItemValueText;
    private GameObject infoButtonPanel;
    public Item infoPanelItem;
    public Text totalGoldText;

    [Header("Purchase Confirmation")]
    public GameObject confirmationPanel;
    public Text confirmationItemName;
    public Text confirmationItemValue;
    public GameObject inventoryObject;
    private Inventory inventory;
    public Text infoBuySellText;
    public Text confirmationBuySellText;
    public Text confirmationText;

    public bool inventoryOpen = false;
    public bool shopInventoryOpen = false;

    public bool sellingItem = false;
    public bool buyingItem = false;

    public GameObject gameManagerObject;
    private GameObject player;
    private PlayerStats playerStats;
    private GameManager GM;


    public void Awake()
    {
        confirmationPanel.SetActive(true);
        infoItemNameText = GameObject.Find("ShopInfoItemName").GetComponent<Text>();
        infoItemDescriptionText = GameObject.Find("ShopInfoItemDescription").GetComponent<Text>();
        infoItemValueText = GameObject.Find("ShopInfoItemValue").GetComponent<Text>();
        infoButtonPanel = GameObject.Find("Shop Info Button Panel");
        print(infoPanelItem + " at Start");
        inventory = inventoryObject.GetComponent<Inventory>();
        infoInventoryPanel.SetActive(false);
        confirmationPanel.SetActive(false);
        GM = gameManagerObject.GetComponent<GameManager>();
        player = GameObject.Find("Player");
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        LoadShopInventory();

    }

    public void Start()
    {
        LoadShopInventory();
    }

    void Update()
    {
        if (shopInventoryOpen == true)
        {
            sellingItem = false;
            buyingItem = true;
        }

        else if (inventoryOpen == true)
        {
            buyingItem = false;
            sellingItem = true;
        }

        if(sellingItem == true)
        {
            infoBuySellText.text = "Sell";
            confirmationBuySellText.text = "Sell";
            confirmationText.text = "Do you wish to sell this item?";
        }

        if (buyingItem == true)
        {
            infoBuySellText.text = "Buy";
            confirmationBuySellText.text = "Buy";
            confirmationText.text = "Do you wish to buy this item?"; 
        }

    }

    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i] == null)
            {
                shopItems[i] = itemToAdd;
                shopItemImages[i].sprite = itemToAdd.sprite;
                shopItemImages[i].enabled = true;
                shopItemSlots[i].GetComponent<ShopSlot>().slotItem = itemToAdd;
                print("Added " + itemToAdd + " to the shop");
                return;
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i] == itemToRemove)
            {
                shopItems[i] = null;
                shopItemImages[i].sprite = null;
                shopItemImages[i].enabled = false;
                shopItemSlots[i].GetComponent<ShopSlot>().slotItem = null;
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
        totalGoldText.text = "Total Gold: " + playerStats.gold.ToString();
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
        totalGoldText.text = "Total Gold: " + playerStats.gold.ToString();
    }

    public void DisableInfoPanel()
    {
        infoInventoryPanel.SetActive(false);
        infoItemNameText.text = null;
        infoItemDescriptionText.text = null;
        infoItemValueText.text = null;
        infoPanelItem = null;
        infoButtonPanel.SetActive(false);
        totalGoldText.text = null;
    }

    public void ConfirmItem()
    {
        confirmationPanel.SetActive(true);
        confirmationItemName.text = infoPanelItem.itemName;
        confirmationItemValue.text = "Item Cost: " + infoPanelItem.itemValue.ToString();
    }

    public void PurchaseItem()
    {
        if (buyingItem == true)
        {
            if (infoPanelItem.itemValue < playerStats.gold)
            {
                inventory.AddItem(infoPanelItem);
                confirmationPanel.SetActive(false);
                // Remove Gold from Player Stats
                playerStats.gold -= infoPanelItem.itemValue;
                totalGoldText.text = "Total Gold: " + playerStats.gold;
            }
        }
        
        else if (sellingItem == true)
        {
            inventory.RemoveItem(infoPanelItem);
            confirmationPanel.SetActive(false);
            // Add Gold to Player Stats
            playerStats.gold += infoPanelItem.itemValue;
            totalGoldText.text = "Total Gold: " + playerStats.gold;
            inventoryOpen = false;
            LoadPlayerInventory();
        }
        
    }

    public void WaitItem()
    {
        confirmationPanel.SetActive(false);
    }

    public void LoadPlayerInventory()
    {
        if (inventoryOpen == false)
        {
            inventoryOpen = true;
            shopInventoryOpen = false;
            print("LOAD PLAYER INVENTORY: STARTING THE FOREACH");
            foreach (Item shopItemToRemove in shopItems) //GameObject slot in shopItemSlots)
            {

                if (shopItemToRemove == null)
                {
                    break;
                }

                else
                {
                    RemoveItem(shopItemToRemove);
                }
            }
            print("shop inventory has been cleared");

            print("prepping to add inventory to shop screen");
            foreach (Item inventoryItem in inventory.items)
            {
                if (inventoryItem == null)
                {
                    break; 
                }

                else
                {
                    print("adding " + inventoryItem + " to the shop inventory screen from inventory ");
                    AddItem(inventoryItem);
                }
            }

            buyingItem = false;
            sellingItem = true;
            confirmationPanel.SetActive(false);
            DisableInfoPanel();
        }
    }

    public void LoadShopInventory()
    {
        if (shopInventoryOpen == false)
        {
            shopInventoryOpen = true;
            inventoryOpen = false;
            foreach (Item shopItemToRemove in inventory.items) //GameObject slot in shopItemSlots)
            {

                if (shopItemToRemove == null)
                {
                    break;
                }

                else
                {
                    RemoveItem(shopItemToRemove);
                }
            }

            foreach (Item item in startItems)
            {
                if (item == null)
                {
                    return;
                }

                else
                {
                    AddItem(item);
                }
            }

            foreach (GameObject slot in shopItemSlots)
            {
                ShopSlot shopSlot = slot.GetComponent<ShopSlot>();
                shopSlot.UpdateSlotshopItems();
            }

            sellingItem = false;
            buyingItem = true;
            confirmationPanel.SetActive(false);
            DisableInfoPanel(); 
        }
    }

    public void CloseShop()
    {
        GM.CloseShop();
    }
}