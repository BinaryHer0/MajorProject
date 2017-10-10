using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueQuestManager : MonoBehaviour
{
    public QuestTrigger questTrigger;
        //USED FOR QUEST DIALOGUE MANAGEMENT
    [Header("Dialogue UI")]
        [Tooltip("Dialogue Box UI")]
    public GameObject textBox;
        [Tooltip("Dialogue Box Text Box")]
    public Text theText;
    public GameObject nameBox;
    public Text nameBoxText;

    [Header("Private variables for the Tag")]
    private GameObject npc;
    private GameObject signPosts;
    private GameObject playerTag;

        [Tooltip("This TextAsset variable is not needed - this is only needed if we are using the DialogueManager as the acting ActivateDialogue")]
    private TextAsset textFile;
        [Tooltip("This will determine the size of the lines of the set Textfile whilst also showing what the text is going to say")]
    public string[] textLines;

    [Header("Current and End lines for the Dialogue")]
        [Tooltip("This will automatically update to reflect the Textfile currently being used")]
    public int currentLine;
    public int endLine;

    [Header("Player Controller")]
    public PlayerController player;

    [Header("Is the Dialogue Active, Does Movement Stop")]
    private ActivateDialogue dialoguePortrait;
    public bool dialogueActive;
    public bool stopMovement;

    [Header("Dialogue Text Speed")]
    private bool isTyping;
    private bool cancelTyping;
    public float typeSpeed;


    void Start()
    {
        npc = GameObject.FindGameObjectWithTag("NPC");
        signPosts = GameObject.FindGameObjectWithTag("World Posts");
        playerTag = GameObject.FindGameObjectWithTag("Player");
        player = FindObjectOfType<PlayerController>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endLine == 0)
        {
            endLine = textLines.Length - 1;
        }

        if (dialogueActive)
        {
            EnableDialogueBox();
                //If the bool for dialogueActive is set to true enable the DialogueBox
        }

        else
        {
            DisableDialogueBox();
                //If the bool for dialogueActive is set to false disable the DialogueBox
        }
    }

    void Update()
    {
        if (!dialogueActive)
        {
            return;
                //If the dialogue bool is not active - return here
        }

        //theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;

                if (currentLine > endLine)
                {
                    DisableDialogueBox();
                    player.dialogueMovement = true;
                        //Once the dialogue has reached the endline (end of the Textfile) then it will enable playerMovement, return back to !dialogueActive unchecking the bool and disabling the Dialogue Box UI
                }

                else
                {
                    StartCoroutine(ScrollingText(textLines[currentLine]));
                        //Scrolling text similar to Pokemon
                }
            }

            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    private IEnumerator ScrollingText (string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;

        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }

        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableDialogueBox()
    {
        //if (stopMovement)
        //{
        //    player.dialogueMovement = false;
        //}

        //questTrigger.dialogueAudioStart


        //GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //GM.shopCanvasGroup.SetActive(true);

        //QuestTrigger QT = GameObject.Find("Trigger Zone - Main Q 0").GetComponent<QuestTrigger>();
        //QT.

        textBox.SetActive(true);
        nameBox.SetActive(true);
        dialogueActive = true;
        player.dialogueMovement = false;

        npc.GetComponent<Collider2D>().enabled = false;
        signPosts.GetComponent<Collider2D>().enabled = false;
        playerTag.GetComponent<Collider2D>().enabled = false;
            //These three will disable the gameObjects (with the specified tags) 2D Box Collider component that has the Trigger box ticked (so make sure this is at the top/forefront)
            //Doing so will make it so that the Dialogue will proceed rather than repeating the same line creating that issue we saw ages ago

        StartCoroutine(ScrollingText(textLines[currentLine]));
            //Scrolling text
}

    public void DisableDialogueBox()
    {
        textBox.SetActive(false);
        dialogueActive = false;
        nameBox.SetActive(true);
        player.dialogueMovement = true;

        npc.GetComponent<Collider2D>().enabled = true;
        signPosts.GetComponent<Collider2D>().enabled = true;
        playerTag.GetComponent<Collider2D>().enabled = true;
            //These three will enable the gameObjects (with the specified tags) 2D Box Collider component that has the Trigger box ticked (so make sure this is at the top/forefront)
            //Doing so will make it so that the player will be able to talk to the NPC again and gain their movement again
    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
                //When the Dialogue has reached the endline it will reload the Script/Textfile so that it will continue to the next line
                //If there is no next line then it will start the currentline > end line
        }
    }
}