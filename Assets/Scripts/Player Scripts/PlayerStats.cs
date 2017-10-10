using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Key Player Stats")]
    public float playerMaxHealth;
    public float playerHealth;
    public int playerLevel;
    public int currentXP;

    public float maxDefence;
    public float currentDefence;

    public float stamina;
    public float maxStamina;
    public float agility;
    public float strength;

    public int gold;

    [Header("Player Position an d Camera Bounds")]
    public float playerPosX;
    public float playerPosY;

    public float minCameraBoundX;
    public float minCameraBoundY;
    public float minCameraBoundZ;
    public float maxCameraBoundX;
    public float maxCameraBoundY;
    public float maxCameraBoundZ;

    public int[] levelUpArray;

    private PlayerManager playerManager;
    private PauseManager pauseManager;
    public CameraFollowTwo cameraBounds;
    public GameManager gameManager;

    [Header("Battle Canvas Texts")]
    // public Text playerBattleHealthText;
    // public Text playerBattleXPText;
    // public Text playerStaminaText;
    // public Text playerBattleLevelText;

    [Header("Debug Panel Texts")]
    public Text playerLevelText;
    public Text playerHealthText;
    public Text playerXPText;

    [Header("Inventory Saving Variables")]
    public Inventory inventory;
    public Item[] savedItems = new Item[12];
    public List<Item> loadedItems = new List<Item>();

    public int item1;
    public int item2;
    public int item3;


    // Use this for initialization
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerHealth = playerMaxHealth;

        pauseManager = GameObject.Find("Player").GetComponent<PauseManager>();
        cameraBounds = GameObject.Find("World Camera").GetComponent<CameraFollowTwo>();

        // Battle Canvas Texts
        // playerBattleHealthText.GetComponent<Text>();
        // playerBattleXPText.GetComponent<Text>();
        // playerStaminaText.GetComponent<Text>();
        // playerBattleLevelText.GetComponent<Text>();

        // Debug Panel Texts
        playerHealthText.GetComponent<Text>();
        playerLevelText.GetComponent<Text>();
        playerXPText.GetComponent<Text>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {



        playerXPText.text = "Current XP: " + currentXP;
        stamina = maxStamina;

        if (currentXP >= levelUpArray[playerLevel])
        {
            playerLevel++;
        }

        playerHealthText.text = "Player Health: " + playerHealth;

        // Battle Stats
        // playerBattleHealthText.text = "HP: " + playerHealth + "/" + playerMaxHealth;
        // playerStaminaText.text = "STA: " + stamina + "/" + maxStamina;
        // playerLevelText.text = "LVL: " + playerLevel;
        // playerBattleLevelText.text = "Lvl: " + playerLevel;
        // playerBattleXPText.text = "XP: " + currentXP;

        // Player Position
        playerPosX = this.transform.position.x;
        playerPosY = this.transform.position.y;

        /* Camera Bounds
        minCameraBoundX = cameraBounds.minCameraPos.x;
        minCameraBoundY = cameraBounds.minCameraPos.y;
        minCameraBoundZ = cameraBounds.minCameraPos.z;
        maxCameraBoundX = cameraBounds.maxCameraPos.x;
        maxCameraBoundY = cameraBounds.maxCameraPos.y;
        maxCameraBoundZ = cameraBounds.maxCameraPos.z;
        */
    }

    public void AddExperience (int experienceToAdd)
    {
        currentXP += experienceToAdd;
    }

    public void SaveOne()
    {
        //this.PullInventory();
        SaveLoadManager.SavePlayerOne(this);
    }

    public void SaveTwo()
    {
        //this.PullInventory();
        SaveLoadManager.SavePlayerTwo(this);
    }

    public void SaveThree()
    {
        //this.PullInventory();
        SaveLoadManager.SavePlayerThree(this);
    }

    public void LoadOne()
    {
        float[] loadedStats = SaveLoadManager.LoadPlayerStatsOne();

        playerMaxHealth = loadedStats[0];
        playerHealth = loadedStats[1];
        stamina = loadedStats[2];
        maxStamina = loadedStats[3];
        agility = loadedStats[4];
        strength = loadedStats[5];
        maxDefence = loadedStats[6];
        currentDefence = loadedStats[7];
        playerPosX = loadedStats[8];
        playerPosY = loadedStats[9];
        minCameraBoundX = loadedStats[10];
        minCameraBoundY = loadedStats[11];
        minCameraBoundZ = loadedStats[12];
        maxCameraBoundX = loadedStats[13];
        maxCameraBoundY = loadedStats[14];
        maxCameraBoundZ = loadedStats[15];

        int[] loadedLevels = SaveLoadManager.LoadPlayerLevelsOne();

        playerLevel = loadedLevels[0];
        currentXP = loadedLevels[1];
        gold = loadedLevels[2];

        transform.position = new Vector3(playerPosX, playerPosY, 0);
        cameraBounds.LoadBounds();
        //this.SendInventory();
    }

    public void LoadTwo()
    {
        float[] loadedStats = SaveLoadManager.LoadPlayerStatsTwo();

        playerMaxHealth = loadedStats[0];
        playerHealth = loadedStats[1];
        stamina = loadedStats[2];
        maxStamina = loadedStats[3];
        agility = loadedStats[4];
        strength = loadedStats[5];
        maxDefence = loadedStats[6];
        currentDefence = loadedStats[7];
        playerPosX = loadedStats[8];
        playerPosY = loadedStats[9];
        minCameraBoundX = loadedStats[10];
        minCameraBoundY = loadedStats[11];
        minCameraBoundZ = loadedStats[12];
        maxCameraBoundX = loadedStats[13];
        maxCameraBoundY = loadedStats[14];
        maxCameraBoundZ = loadedStats[15];

        int[] loadedLevels = SaveLoadManager.LoadPlayerLevelsTwo();

        playerLevel = loadedLevels[0];
        currentXP = loadedLevels[1];
        gold = loadedLevels[2];

        transform.position = new Vector3(playerPosX, playerPosY, 0);
        cameraBounds.LoadBounds();
        //this.SendInventory();
    }

    public void LoadThree()
    {
        float[] loadedStats = SaveLoadManager.LoadPlayerStatsThree();

        playerMaxHealth = loadedStats[0];
        playerHealth = loadedStats[1];
        stamina = loadedStats[2];
        maxStamina = loadedStats[3];
        agility = loadedStats[4];
        strength = loadedStats[5];
        maxDefence = loadedStats[6];
        currentDefence = loadedStats[7];
        playerPosX = loadedStats[8];
        playerPosY = loadedStats[9];
        minCameraBoundX = loadedStats[10];
        minCameraBoundY = loadedStats[11];
        minCameraBoundZ = loadedStats[12];
        maxCameraBoundX = loadedStats[13];
        maxCameraBoundY = loadedStats[14];
        maxCameraBoundZ = loadedStats[15];

        int[] loadedLevels = SaveLoadManager.LoadPlayerLevelsThree();

        playerLevel = loadedLevels[0];
        currentXP = loadedLevels[1];
        gold = loadedLevels[2];

        transform.position = new Vector3(playerPosX, playerPosY, 0);
        cameraBounds.LoadBounds();
        //this.SendInventory();
    }

    public void PullInventory()
    {
        savedItems = inventory.items;
        item1 = savedItems[0].itemID;
    }

    public void SendInventory()
    {
        foreach(Item item in inventory.items) // removing items from the inventory
        {
            inventory.RemoveItem(item);
            break;
        }

        // BELOW, FIND A WAY OF CHECKING THE INT ITEM VALUE AGAINST THAT LIST
        

        // THIS IS FOR ADDING THE ITEM TO THE INVENTORY
        foreach (Item item in loadedItems) // checking all items in the loaded items list
        {
            foreach (Item ID in gameManager.itemDatabase) // if item ID = item ID of item database 
            {
                if (ID.itemValue == item.itemID)
                {
                    inventory.AddItem(item); // adds it to the inventory
                }

                else
                {
                    break; // goes to next item
                }
            }
        }
        /*
        foreach(Item item in savedItems)
        {
            inventory.AddItem(item);
            break; 
        }
        */
    }
}