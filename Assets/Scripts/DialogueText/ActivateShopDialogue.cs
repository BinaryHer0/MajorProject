using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateShopDialogue : MonoBehaviour
{
        //USED FOR SHOP DIALOGUE
    [Header("Calling on the Dialogue Manager Scripts")]
    public DialogueShopManager dialogueManager;

    [Header("NPC .text File and the Canvas holding the Character Potrait")]
    public TextAsset theText;
    public Transform charPortrait;
    public string nameText;

    [Header("Current and End lines for the Dialogue")]
    public int startLine;
    public int endLine;

    [Header("Do you want to destroy the GameObject?")]
    public bool destroy;

    [Header("Do you want the Dialogue to be activated via button press?")]
    public bool buttonPress;
    private bool waitForPress;

    [Header("Shop Variables")]
    public bool isAShop;
    public bool shopStarted;
    public GameObject shopCanvas;
        [Tooltip("NPC Shopkeeper")]
    public GameObject objectWithDialogue;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueShopManager>();
    }

    void Update()
    {
        if (waitForPress && Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.ReloadScript(theText);
            dialogueManager.currentLine = startLine;
            dialogueManager.endLine = endLine;
            dialogueManager.EnableDialogueBox();
            charPortrait.gameObject.SetActive(true);
            dialogueManager.nameBoxText.text = "" + nameText;
            isAShop = true;
            //These will call upon the Dialogue Manager script - Reloads the Text File so that it continue when it reaches the end line.
            //Set the Shop to always be true

            if (charPortrait.gameObject.activeInHierarchy == false)
            {
                charPortrait.gameObject.SetActive(true);
            }
        }

        if (waitForPress && Input.GetKeyDown(KeyCode.Return))
        {
            dialogueManager.ReloadScript(theText);
            dialogueManager.currentLine = startLine;
            dialogueManager.endLine = endLine;
            dialogueManager.EnableDialogueBox();
            charPortrait.gameObject.SetActive(true);
            dialogueManager.nameBoxText.text = "" + nameText;
            isAShop = true;
            //These will call upon the Dialogue Manager script - Reloads the Text File so that it continue when it reaches the end line.
            //Set the Shop to always be true

            if (charPortrait.gameObject.activeInHierarchy == false)
            {
                charPortrait.gameObject.SetActive(true);
            }
        }

        //if (objectWithDialogue)
        //{
        //    if(dialogueManager.currentLine > endLine)
        //    {
        //        if (dialogueManager.endLine == endLine)
        //        {
        //            ShopCanvas();
        //        }
        //    }
        //}

        if (objectWithDialogue.gameObject.name == "NPC - Weaponshop")
        {
            if (objectWithDialogue == dialogueManager.currentLine > endLine)
            {
                ShopCanvas();
                //isAShop = true;
            }
        }
        //When the dialogue has reached the end of it's line, for example is the starting line is 0 and the ending line is 3 when it has exceeded 3 it will call the Shop Canvas function

        //I wanted to do what I did with the DialogueManager where we can have both Input E and Input Return (Enter) in the same section, however when they are it creates a bug/confusion
        //So far now they will have to be separate unless we find a way to do that (TO DO LIST!!!!!!!)
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //isAShop = true;

            if (buttonPress)
            {
                waitForPress = true;
                return;
            }
            objectWithDialogue.GetComponent<ActivateShopDialogue>().isAShop = true;
            dialogueManager.ReloadScript(theText);
            dialogueManager.currentLine = startLine;
            dialogueManager.endLine = endLine;
            dialogueManager.EnableDialogueBox();
            dialogueManager.nameBoxText.text = "" + nameText;

            if (charPortrait.gameObject.activeInHierarchy == false)
            {
                charPortrait.gameObject.SetActive(true);
            }
            if (destroy)
            {
                Destroy(gameObject);
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            waitForPress = false;
            if (charPortrait.gameObject.activeInHierarchy == false)
            {
                charPortrait.gameObject.SetActive(false);
                //If the waitForPress is set to false and the endline is true then it will disable the character portrait
            }
        }
    }

    public void ShopCanvas()
    {
        if (objectWithDialogue.gameObject.name == "NPC - Weaponshop")
        //Find the gameObject name - NPC - Weaponshop | Will need to do this for the NPC - Armour and NPC - Consumables
        {
            if (isAShop == true)
            {
                //objectWithDialogue.GetComponent<ActivateShopDialogue>().isAShop = true;

                objectWithDialogue.GetComponent<ActivateShopDialogue>().shopStarted = true;
                //Sets the isAShop and shopStarted bools to true on this specified gameObject

                GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
                GM.shopCanvasGroup.SetActive(true);
                shopCanvas.SetActive(true);

                waitForPress = false;

                if (isAShop == false)
                    {
                        dialogueManager.CloseShop();
                        waitForPress = false;
                    }
            }
            //else if (isAShop == false)
            //{
            //    dialogueManager.CloseShop();
            //    waitForPress = false;
            //}
        }
    }
}
