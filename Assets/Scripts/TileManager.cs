using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour {

	//Current Tile
	public GameObject currentTile;
    public string tileName;
    public string region;

	// Whether Player is detected
	private bool playerDetected = false;

    // Player
    public GameObject playerObject;

    // Random Number Generator
    private int randomNumber;

    // Tile Percentage Chance
    [Header("Percentages")]
    public int tilePercentage;

    // Battle Systems
    public GameObject battleManager;
    private TileManager tileManager;
    public GameObject gameManager;

    // Debug Viewer
    [Header("Debug Options")]
    public Text detectionText;
    public Text randomNumberText;
    public Text battleInvokedText;
    // public Text battleStartedText;
    public Text enemyLevelText;


    // Use this for initialization
    void Start () 
	{
		currentTile = gameObject;
		detectionText.GetComponent<Text> ();
        randomNumberText.GetComponent<Text>();
        battleInvokedText.GetComponent<Text>();
        battleManager.GetComponent<BattleManager>();
        currentTile.GetComponent<TileManager>();
        gameManager.GetComponent<GameManager>();
        region = this.gameObject.tag;
        
    }

    // Player Trigger Detector
    void OnTriggerEnter2D(Collider2D col)
    {
            if (col.CompareTag("Player"))
            {
                playerObject = col.gameObject;
                playerDetected = true;
                detectionText.text = "Player Detected";
                RandomNumber();
                InvokeRepeating("RandomNumber", 3f, 2f);
                print("Player is in a trigger zone");
            }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        playerObject = null;
        playerDetected = false;
        detectionText.text = "Player Not Detected";
        randomNumberText.text = "Random Number: ";
        CancelInvoke("RandomNumber");
        battleInvokedText.text = "Battle Invoked: False";
        print("Player has left trigger zone");
    }

    // Generates Random Number to initiate Battle Scene
    void RandomNumber()
    {

		randomNumber = Random.Range(0, 100);
        randomNumberText.text = ("Random Number: " + randomNumber.ToString());

        if (randomNumber < tilePercentage && gameManager.GetComponent<GameManager>().invokeBattleCount == 0)
        {
            battleInvokedText.text = "Battle Invoked: True";
            gameManager.GetComponent<GameManager>().invokeBattleCount++;
            InvokeBattle();
            randomNumber = 0;
        }

        else
        {
            battleInvokedText.text = "Battle Invoked: False";
        }
    }

    public void InvokeBattle()
    {
        if (gameManager.GetComponent<GameManager>().invokeBattleCount == 1)
        {

            if(region == "Forest Region") // Region 0
            {
                gameManager.GetComponent<GameManager>().currentRegion = 0;
            }

            if(region == "Forest Cave") // Region 1
            {
                gameManager.GetComponent<GameManager>().currentRegion = 1;
            }

            gameManager.GetComponent<GameManager>().Encounter();
            print("Invoke Battle");

            
        }
    }
}
