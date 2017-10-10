using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

    [Header("World Locations")]
    public GameObject worldParent;
    public GameObject battleArea;

    [Header("Cameras")]
    public GameObject worldCamera;
    public GameObject battleCamera;

    [Header("Enemy Spawn Points")]
    public List<Transform> enemySpawnPoints = new List<Transform>();
    public string deathScene;

    [Header("Hero Spawn Points")]
    public GameObject heroSpawnTop;
    public GameObject heroSpawnMiddle;
    public GameObject heroSpawnBottom;

    [Header("Player Objects")]
    public GameObject playerWorld;
    public GameObject hero;

    [Header("Battle Canvas")]
    // Panels
    public GameObject battleCanvas;
    public GameObject actionsPanel;
    public GameObject enemySelectPanel;
    public GameObject skillsPanel;
    public GameObject heroPanel;
    // Texts
    public Text endBattleText;
    // Spacer Objects in Battle Canvas
    public Transform actionSpacer;
    public Transform buttonSpacer;
    public Transform skillSpacer;
    // Prefabs
    [Header("Battle Canvas Prefabs")]
    public GameObject actionButton;
    public GameObject enemyButton;
    public GameObject skillButton;

    [Header("States")]
    public PerformAction battleStates;
    public HeroGUI HeroInput;

    [Header("Lists")]
    public List<TurnBasedHandler> PerformList = new List<TurnBasedHandler>();
    public List<GameObject> HerosInGame = new List<GameObject>();
    public List<GameObject> EnemiesInBattle = new List<GameObject>();
    public List<GameObject> HerosToManage = new List<GameObject>();
    public List<GameObject> EnemiesToManage = new List<GameObject>();
    public List<GameObject> DeadEnemies = new List<GameObject>();

    // List for Attack Buttons
    private List<GameObject> attackButtons = new List<GameObject>();
    // List for Enemy Buttons
    private List<GameObject> enemyButtons = new List<GameObject>();

    // Enums
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE,
        RESET
    }

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE,
        RESET
    }

    [Header("Battle Variables")]
    public int escapeChance = 0;
    public bool heroTurns = false;
    public bool enemyTurns = false;
    public bool performTurn = false;
    public bool enableDebugLines = false;

    // Private Variables
    private TurnBasedHandler HeroChoice;
    private TileManager tileManager;
    private PlayerStats playerStats;
    public GameObject gameManager;

    // End Battle Variables
    private bool endBattle = false;
    private bool resetBattle = false;
    private bool battleWin = false;
    private bool battleLose = false;
    private bool battleEscape = false;
    private bool escapeBattleFail = false;

    public void BattleLoad()
    {
        // heroSpawnMiddle.transform.position = transform.position;
        
        // Moved from Void Start 
       // tileManager = GameObject.Find("DetectorTile").GetComponent<TileManager>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        battleArea.SetActive(true);

        SpawnEnemies();

        battleStates = PerformAction.WAIT;
        HeroInput = HeroGUI.ACTIVATE;

        HerosInGame.AddRange(GameObject.FindGameObjectsWithTag("Hero"));

        endBattleText.GetComponent<Text>();
        endBattleText.gameObject.SetActive(false);

        battleCanvas.SetActive(true);
        actionsPanel.SetActive(true);
        enemySelectPanel.SetActive(true); // setting everything active to make sure buttons spawn
        skillsPanel.SetActive(true);

        EnemyButtons(); // Spawns Enemy Buttons based of how many

        // Starting the Battle
        worldParent.SetActive(false);
        worldCamera.SetActive(false);
        playerWorld.SetActive(false);

        BattleStart();

    }
    
    void SpawnEnemies ()
    {
        for (int i = 0; i < GameManager.instance.enemyAmount; i++)
        {
            GameObject newEnemy = Instantiate(GameManager.instance.enemiesToBattle[i], enemySpawnPoints[i].position, Quaternion.identity) as GameObject;
            newEnemy.name = newEnemy.GetComponent<EnemyState>().enemy.theName + "_" + (i + 1);
            newEnemy.GetComponent<EnemyState>().enemy.theName = newEnemy.name;
            EnemiesInBattle.Add(newEnemy);
        }
    }

    public void BattleStart()
    {
        ClearAttackPanel();
        actionsPanel.SetActive(false);
        enemySelectPanel.SetActive(false);
        skillsPanel.SetActive(false);
        battleCamera.SetActive(true);

        foreach (GameObject hero in HerosInGame)
        {
            MainHeroState heroState = hero.GetComponent<MainHeroState>();
            heroState.UpdatePlayerLevel();
            heroState.currentState = MainHeroState.TurnState.PROCESSING;
            heroState.playerTurn = true;
            heroTurns = true;
        }
    }

	
	void Update ()
    {
        if (enableDebugLines) print("Battle Manager State: " + battleStates);
        switch (battleStates)
        {
            case (PerformAction.WAIT):

                if (PerformList.Count > 0)
                    {
                        battleStates = PerformAction.TAKEACTION;
                    }

                if (enemyTurns == true)
                {
                    if (enableDebugLines) print("BATTLE MANAGER: Enemy Turn is True");
                    foreach (GameObject enemy in EnemiesInBattle)
                    {
                        EnemyState ES = enemy.GetComponent<EnemyState>();
                        ES.enemyTurn = true;
                        ES.currentState = EnemyState.TurnState.PROCESSING;
                        if (enableDebugLines) print("BATTLE MANAGER " + enemy.name + " has set enemyTurn to true");
                    }

                    enemyTurns = false;
                }

                if (heroTurns == true)
                {
                    foreach (GameObject hero in HerosInGame)
                    {
                        MainHeroState MS = hero.GetComponent<MainHeroState>();
                        MS.playerTurn = true;
                    }
                }

                break;

            case (PerformAction.TAKEACTION):
                if (enableDebugLines) print("Checking the perform list"); 
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                if (PerformList[0].Type == "Enemy")
                {
                    EnemyState enemyState = performer.GetComponent<EnemyState>();
                        for(int i = 0; i < HerosInGame.Count; i++)
                        {
                            if(PerformList[0].AttackersTarget == HerosInGame[i])
                            {
                                enemyState.HeroToAttack = PerformList[0].AttackersTarget;
                                enemyState.currentState = EnemyState.TurnState.ACTION;
                            break;
                            }
                        else
                        {
                            PerformList[0].AttackersTarget = HerosInGame[Random.Range(0, HerosInGame.Count)];
                            enemyState.HeroToAttack = PerformList[0].AttackersTarget;
                            enemyState.currentState = EnemyState.TurnState.ACTION;
                        }
                    }

                }

                if (PerformList[0].Type == "Hero")
                {
                    MainHeroState heroState = performer.GetComponent<MainHeroState>();
                    heroState.enemyToAttack = PerformList[0].AttackersTarget;
                    heroState.currentState = MainHeroState.TurnState.ACTION;
                }

                if (enableDebugLines) print(performer + " has been collected from the perform action");

                battleStates = PerformAction.PERFORMACTION;

                    break;

            case (PerformAction.PERFORMACTION):
                 
            break;

            case (PerformAction.CHECKALIVE): // Checks to see whos alive in battle and determine if won/lost/next turn
                if (HerosInGame.Count == 0 || hero.GetComponent<MainHeroState>().alive == false)
                {
                    battleStates = PerformAction.LOSE;
                }

                else if (EnemiesInBattle.Count == 0)
                {
                    battleStates = PerformAction.WIN;
                }

                else
                {
                    ClearAttackPanel();
                    HeroInput = HeroGUI.ACTIVATE;
                }

            break;

            case (PerformAction.WIN):
                {
                    battleWin = true;
                    if (resetBattle == false)
                    {
                        StartCoroutine(EndBattleSequence());
                    }
                }
            break;

            case (PerformAction.LOSE):
                {
                    ClearAttackPanel();
                    battleLose = false;
                    SceneManager.LoadScene(deathScene);
                }
            break;

            case (PerformAction.RESET):

            break;
        }
      
        switch (HeroInput)
        {

            case (HeroGUI.ACTIVATE):
                if(HerosToManage.Count > 0)
                { 
                    HerosToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                    HeroChoice = new TurnBasedHandler();

                    actionsPanel.SetActive(true);
                    CreateActionButtons();
                    HeroInput = HeroGUI.WAITING;
                }

            break;

            case (HeroGUI.WAITING):

            break;

            case (HeroGUI.DONE):

                HeroInputDone();
            break;

            case (HeroGUI.RESET):

                if (enableDebugLines) print("Hero Input in battlemanager in reset state");

            break;

        }

    }

    public void CollectActions(TurnBasedHandler input)
    {
        PerformList.Add(input);
    }

    public void EnemyButtons()
    {
        // Cleaning Up Buttons
        foreach (GameObject enemyBtn in enemyButtons)
        {
            Destroy(enemyBtn);
        }

        enemyButtons.Clear();

        // Creating Buttons
        foreach (GameObject enemy in EnemiesInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            newButton.transform.SetParent(buttonSpacer);
            newButton.transform.localScale = new Vector3(1f, 1f, 1f);
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyState currentEnemy = enemy.GetComponent<EnemyState>();

            Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text>();
            buttonText.text = currentEnemy.enemy.theName;

            button.EnemyPrefab = enemy;
            enemyButtons.Add(newButton);

            if (enableDebugLines) print("Spawning Buttons");
        }
         
    }

    public void Input1() // Attack Button 
    {
        // Begins collecting data for hero attack 
        if (EnemiesInBattle.Count == 0)
        {
            return;
        }

        else
        {
            HeroChoice.Attacker = HerosToManage[0].name;
            HeroChoice.AttackersGameObject = HerosToManage[0];
            HeroChoice.Type = "Hero";
            HeroChoice.choosenAttack = HerosInGame[0].GetComponent<MainHeroState>().hero.attacks[0];

            // Disables Action Panel and Enables Enemy Target Panel
            actionsPanel.SetActive(false);
            enemySelectPanel.SetActive(true);
        }

    }

    public void Input2(GameObject targetEnemy) // Enemy Target Selection
    {
        HeroChoice.AttackersTarget = targetEnemy;
        HeroInput = HeroGUI.DONE;
    }

    public void Input4(BaseAttack choosenSkill) // Choosen Skill to Use
    {
        // Begins collecting data for hero attack
        HeroChoice.Attacker = HerosToManage[0].name;
        HeroChoice.AttackersGameObject = HerosToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.choosenAttack = choosenSkill;
        HeroChoice.AttackersGameObject.GetComponent<MainHeroState>().hero.currentStamina = HeroChoice.AttackersGameObject.GetComponent<MainHeroState>().hero.currentStamina - HeroChoice.choosenAttack.attackCost;
        skillsPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }

    public void Input3() // Switching to Skills Attack
    {
        actionsPanel.SetActive(false);
        skillsPanel.SetActive(true);
    }

    public void BackToActionScreen () // Back to selecting what action to take
    {
        enemySelectPanel.SetActive(false);
        actionsPanel.SetActive(true);
    }

    public void HeroInputDone() // Completed Hero Input and sending info to the perform list plus removing hero from HerosToManage
    {
        PerformList.Add(HeroChoice);
        ClearAttackPanel();

        HerosToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        HerosToManage.RemoveAt (0);
        HeroInput = HeroGUI.ACTIVATE;
    }

    public void Escape() // Escaping Battle Function
    {
        int randomNumber = Random.Range(0, 100);

        if (escapeChance > randomNumber) // if escape chance is higher than random number, then the player escapes, if not he is stuck in battle
        {
            battleEscape = true;
            StartCoroutine(EndBattleSequence());
        }

        else
        {
            endBattleText.gameObject.SetActive(true);
            endBattleText.text = "You have failed to escape";

            ClearAttackPanel();

            StartCoroutine(EscapeFail());

        }
    }

    void ClearAttackPanel()
    {
        enemySelectPanel.SetActive(false);
        actionsPanel.SetActive(false);
        skillsPanel.SetActive(false);
        foreach (GameObject AttackButton in attackButtons)
        {
            Destroy(AttackButton);
        }
        attackButtons.Clear();
    }

    public IEnumerator EndBattleSequence() // ENDING AND RESETING BATTLE STEP 1
     {
         if(endBattle)
         {
             yield break;
         }

        resetBattle = true;

        endBattle = true;
        ClearAttackPanel();
        battleStates = PerformAction.RESET;
        HeroInput = HeroGUI.RESET;
        if (enableDebugLines) print("Hero Input is now Waiting"); 


        for (int i = 0; i < HerosInGame.Count; i++)
        {
            MainHeroState MHS = HerosInGame[i].GetComponent<MainHeroState>();
            MHS.UpdateHeroPanel();
            MHS.UpdatePlayerLevel();
            MHS.enemyToAttack = null;

        }
        print("Has completed the for loop"); 

        // Clearing All of the Lists
        PerformList.Clear();
        HerosInGame.Clear();
        HerosToManage.Clear();
        EnemiesToManage.Clear();

        foreach (GameObject enemy in EnemiesInBattle) // Destroys All Enemies in Battle
        {
            Destroy(enemy);
        }

        foreach (GameObject enemy in DeadEnemies) // Destroys All Dead Enemies in Battle
        {
            Destroy(enemy);
        }
        EnemiesInBattle.Clear(); // Clears the Enemies in Battle List

        if (enableDebugLines) print("has cleared the perform list, heros in game and heros to manage"); 

        if (battleWin == true)
        {
            endBattleText.gameObject.SetActive(true);
            endBattleText.text = "Enemies have been defeated!";
        }

        else if (battleLose == true)
        {
            endBattleText.gameObject.SetActive(true);
            endBattleText.text = "You have been defeated!";
        }

        else if (battleEscape == true)
        {
            endBattleText.gameObject.SetActive(true);
            endBattleText.text = "You have escaped Battle!";
        }

        if (enableDebugLines) print("displayed the battle result text");

        // yield return new WaitForSeconds(3); // THIS LINE BREAKS EVERYTHING
        // print("has waited 3 seconds");

        /*
        playerWorld.SetActive(true);
        worldCamera.SetActive(true);
        print("EndBattleSequence End");
        */
        if (enableDebugLines) print("Calling Battle End");
        BattleEnd();
        endBattle = false;
        resetBattle = false;
 
    }

    public IEnumerator EscapeFail()
    {
        if (escapeBattleFail)
        {
            yield break;
        }

        escapeBattleFail = true;

        yield return new WaitForSeconds(3f);

        endBattleText.text = null;
        endBattleText.gameObject.SetActive(false);

        heroTurns = false;
        HerosToManage[0].GetComponent<MainHeroState>().playerTurn = false;
        enemyTurns = true;
        HerosToManage[0].GetComponent<MainHeroState>().currentState = MainHeroState.TurnState.PROCESSING;
        HerosToManage.Clear();
        HeroInput = HeroGUI.ACTIVATE;

        escapeBattleFail = false;
    }




    public void BattleEnd()
    {
        if (enableDebugLines) print("Battle End Starting");
        HerosToManage.Clear();

        gameManager.GetComponent<GameManager>().BattleReset();

        if (enableDebugLines) print("Battle End finished");
        /*
        battleCamera.SetActive(false);
        actionsPanel.SetActive(false);
        enemySelectPanel.SetActive(false);
        endBattleText.gameObject.SetActive(false);
        battleArea.SetActive(false);
        worldParent.SetActive(true);
        worldCamera.SetActive(true);
        
        print("Battle End");
        */
    }
    

        void CreateActionButtons()
    {
        // Attack Button
        GameObject AttackButton = Instantiate(actionButton) as GameObject;
        Text AttackButtonText = AttackButton.transform.Find("ActionText").GetComponent<Text>();
        AttackButtonText.text = "Attack";
        AttackButton.GetComponent<Button>().onClick.AddListener(() => Input1());
        AttackButton.transform.SetParent(actionSpacer, false);
        attackButtons.Add(AttackButton);


        // Skills Button
        GameObject SkillButton = Instantiate(actionButton) as GameObject;
        Text SkillButtonText = SkillButton.transform.Find("ActionText").GetComponent<Text>();
        SkillButtonText.text = "Skills";
        SkillButton.GetComponent<Button>().onClick.AddListener(() => Input3());
        SkillButton.transform.SetParent(actionSpacer, false);
        attackButtons.Add(SkillButton);

        if(HerosInGame[0].GetComponent<MainHeroState>().hero.Skills.Count>0)
        {
            foreach(BaseAttack skillAttack in HerosInGame[0].GetComponent<MainHeroState>().hero.Skills)
            {
                GameObject selectSkillButton = Instantiate(skillButton) as GameObject;
                Text selectSkillButtonText = selectSkillButton.transform.Find("SkillText").GetComponent<Text>();
                selectSkillButtonText.text = skillAttack.attackName;
                AttackButton ATB = selectSkillButton.GetComponent<AttackButton>();
                ATB.skillAttackToPerform = skillAttack;
                selectSkillButton.transform.SetParent(skillSpacer, false);
                if (skillAttack.attackCost < HerosInGame[0].GetComponent<MainHeroState>().hero.currentStamina)
                {
                    attackButtons.Add(selectSkillButton);
                }
            }
        }
        else
        {
            SkillButton.GetComponent<Button>().interactable = false;
        }

        /*
        // Inventory Button
        GameObject InventoryButton = Instantiate(actionButton) as GameObject;
        Text InventoryButtonText = InventoryButton.transform.Find("ActionText").GetComponent<Text>();
        InventoryButtonText.text = "Inventory";

        InventoryButton.transform.SetParent(actionSpacer, false);
        attackButtons.Add(InventoryButton);
        */


        // Escape Button
        GameObject EscapeButton = Instantiate(actionButton) as GameObject;
        Text EscapeButtonText = EscapeButton.transform.Find("ActionText").GetComponent<Text>();
        EscapeButtonText.text = "Escape!";
        EscapeButton.GetComponent<Button>().onClick.AddListener(() => Escape());

        EscapeButton.transform.SetParent(actionSpacer, false);
        attackButtons.Add(EscapeButton);
    }
     


}

