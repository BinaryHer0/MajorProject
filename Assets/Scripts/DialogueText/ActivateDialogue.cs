using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateDialogue : MonoBehaviour
{
        //USED FOR REGULAR DIALOGUE
    [Header("Calling on the Dialogue Manager Scripts")]
    public DialogueBoxManager dialogueManager;

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

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueBoxManager>();
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
            //These will call upon the Dialogue Manager script - Reloads the Text File so that it continue when it reaches the end line.

            if (charPortrait.gameObject.activeInHierarchy == false)
            {
                charPortrait.gameObject.SetActive(true);
            }

            //if (destroy)
            //{
            //    Destroy(gameObject);
            //}
                    //Currently not used - Destroys the gameObject upon interaction
        }

        if (waitForPress && Input.GetKeyDown(KeyCode.Return))
        {
            dialogueManager.ReloadScript(theText);
            dialogueManager.currentLine = startLine;
            dialogueManager.endLine = endLine;
            dialogueManager.EnableDialogueBox();
            charPortrait.gameObject.SetActive(true);
            dialogueManager.nameBoxText.text = "" + nameText;
            //These will call upon the Dialogue Manager script - Reloads the Text File so that it continue when it reaches the end line.

            if (charPortrait.gameObject.activeInHierarchy == false)
            {
                charPortrait.gameObject.SetActive(true);
            }

            //if (destroy)
            //{
            //    Destroy(gameObject);
            //}
            //Currently not used - Destroys the gameObject upon interaction
        }

        }
        //I wanted to do what I did with the DialogueManager where we can have both Input E and Input Return (Enter) in the same section, however when they are it creates a bug/confusion
        //So far now they will have to be separate unless we find a way to do that (TO DO LIST!!!!!!!)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {

            if (buttonPress)
            {
                waitForPress = true;
                return;
            }

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
            }
        }
    }
}
