using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    public GameObject inventory;

    [Header("Area Objects")]
    public GameObject battleArea;
    public GameObject world;

    [Header("Canvas and Cameras")]
    public GameObject worldCamera;
    public GameObject battleCamera;
    public GameObject DebugCanvas;
    public GameObject UIOptions;
    public GameObject UIStatus;
    public GameObject UISkills;
    public GameObject UIEquipment;
    public GameObject UIItems;
    public GameObject UIMenu;
    public bool enableDebugCanvas = false;
    public bool disableMusic = false;
    private AudioSource worldCameraAudio;
    private AudioSource battleCameraAudio;

    [Header("Shop Canvas")]
    public GameObject shopCanvasGroup;

    [Header("Battle Variables")]

    public bool enableBattleDebugLines = false;
    public int invokeBattleCount = 0;
    private bool resetCount = false;
    public bool roamingEnemyEncounter = false;
    public bool encounter = false;
    public int enemyAmount;

    public enum EnemyEncounter
    {
        WOLF,
        PIG,
        CENTIPEDE,
        EAGLE,
        BAT
    }
    public EnemyEncounter encounteredEnemy;

    [System.Serializable]
    public class TileManagerData
    {
        public string tileName;
        public int maxAmountEnemies = 3;
        public string battleBackground;
        public List<GameObject> possibleEnemies = new List<GameObject>();
    }
    

    public List<TileManagerData> tileManagerData = new List<TileManagerData>();

    public List<GameObject> enemiesToBattle = new List<GameObject>();

    public int currentRegion;



    [Header("Prefabs")]
    public static GameManager instance;

    [Header("States")]
    public GameStates gameState;

    public enum GameStates
    {
        WORLD,
        SAFE,
        BATTLE,
        IDLE
    }

    // Private Variables
    private BattleManager battleManager;
    private bool testFix = false;
    private bool playerDied = false;

    [Header("Start Items")]
    public Item[] startingItems = new Item[5];
    public Item[] itemDatabase = new Item[0];

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance !=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if(!GameObject.Find("Player"))
        {
            GameObject PlayerSpawn = Instantiate(player, Vector2.zero, Quaternion.identity) as GameObject;
            Vector2 playerSpawnLocation = new Vector2(-1, 1);
            PlayerSpawn.transform.position = playerSpawnLocation;
            PlayerSpawn.name = "Player";
        }

        world.SetActive(true);
        battleArea.SetActive(true);

        battleManager = GameObject.Find("Battle Area").GetComponent<BattleManager>();

        worldCameraAudio = this.GetComponent<AudioSource>();
        battleCameraAudio = battleCamera.GetComponent<AudioSource>();
        UIOptions.SetActive(true);
        UIStatus.SetActive(true);
        UISkills.SetActive(true);
        UIEquipment.SetActive(true);
        UIItems.SetActive(true);
        UIMenu.SetActive(true);

        shopCanvasGroup.SetActive(true);

        GameStart(); // all set up variables (just moved them out of awake function

    }


    public void GameStart() // General Game Start Functions
    {
        world.SetActive(true);
        battleArea.SetActive(false);
        worldCamera.SetActive(true);
        battleCamera.SetActive(false);
        player.SetActive(true);
        PlayerInventoryStart();
        UIOptions.SetActive(false);
        UIStatus.SetActive(false);
        UISkills.SetActive(false);
        UIEquipment.SetActive(false);
        UIItems.SetActive(false);
        UIMenu.SetActive(false);

        shopCanvasGroup.SetActive(false);
    }

    void Update()
    {
        switch(gameState)
        {
            case (GameStates.WORLD):
                if (invokeBattleCount == 1)
                {
                    gameState = GameStates.BATTLE;
                }
                break;

            case (GameStates.SAFE):

                break;

            case (GameStates.BATTLE):
                gameState = GameStates.IDLE;
                break; 

            case (GameStates.IDLE):
                if (invokeBattleCount == 0)
                {
                    gameState = GameStates.WORLD;
                }
                break;
        }

        if (enableDebugCanvas == false)
        {
            DebugCanvas.SetActive(false);
        }

        if (disableMusic == true)
        {
            worldCameraAudio.enabled = false;
            battleCameraAudio.enabled = false;
        }
    }

    public void BattleReset()
    {
        StartCoroutine(InvokeBattleReset());
    }

    public IEnumerator InvokeBattleReset()
    {

        if (enableBattleDebugLines) print("Invoking Battle Reset");
        if (resetCount)
        {
            yield break;
        }

        resetCount = true;
        testFix = true;

        while (testFix == true)
        {
            yield return new WaitForSeconds(3f);
            if (enableBattleDebugLines) print("Waited 3 Seconds");
            testFix = false;
        }
        battleManager.battleCamera.SetActive(false);
        battleManager.actionsPanel.SetActive(false);
        battleManager.enemySelectPanel.SetActive(false);
        battleManager.endBattleText.gameObject.SetActive(false);
        roamingEnemyEncounter = false;
        battleManager.worldParent.SetActive(true);
        battleManager.playerWorld.SetActive(true);
        battleManager.battleCanvas.SetActive(false);
        battleManager.worldCamera.SetActive(true);
        if (enableBattleDebugLines) print("Section 1 of Battle Reset Complete");

        yield return new WaitForSeconds(5f);
        if (enableBattleDebugLines) print("Waited for 5 seconds"); 

        battleManager.battleStates = BattleManager.PerformAction.WAIT;
        enemiesToBattle.Clear();
        invokeBattleCount = 0;
        
        // Resetting whos turn it is
        battleManager.enemyTurns = false;
        battleManager.heroTurns = false;

        gameState = GameManager.GameStates.WORLD;

        if (enableBattleDebugLines) print("End of Battle Reset");

        battleManager.HerosInGame.Clear();
        battleManager.HerosToManage.Clear();
        battleArea.SetActive(false);
        resetCount = false;
        
    }

    public void Encounter()
    {
        if (enableBattleDebugLines) print("Encounter");
        enemyAmount = Random.Range(1, tileManagerData[currentRegion].maxAmountEnemies + 1);
        for (int i = 0; i < enemyAmount; i++)
        {
            enemiesToBattle.Add(tileManagerData[currentRegion].possibleEnemies[Random.Range(0, tileManagerData[currentRegion].possibleEnemies.Count)]);
        }

        battleManager.BattleLoad();
        player.SetActive(false);
    }

    public void PlayerInventoryStart ()
    {
        foreach (Item item in startingItems)
        {
            Inventory playerInventory = inventory.GetComponent<Inventory>();
            playerInventory.AddItem(item);

        }
    }

    public void CloseShop()
    {
        shopCanvasGroup.SetActive(false);
        
    }
}
