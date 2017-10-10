using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    [Header("Calling on the Quest and Dialogue Manager Scripts")]
    private QuestManager questManager;
    private DialogueQuestManager dialogueManager;

    [Header("Make sure to drag the Starting Quest Gameobject (with this Script) into the Quest Manager")]
    [Header("Quest Information")]
    public int questNumber;
    public string nameText;

    [Tooltip("Will it be the starting point or end point?")]
    public bool startQuest;
    public bool endQuest;

        [Tooltip("Is this a Main Q or Side Q")]
    public bool isMainQuest;
    public bool isSideQuest;

    [Header("Text File")]
        [Tooltip("This is the .txt file that you want for the dialogue")]
    public TextAsset theText;

    [Header("Start of the Text File")]
        [Tooltip("Start line and end line of the .txt file, note that in Unity/VS it says 1 but it is 0.")]
    public int startLine;
    public int endLine;

    [Header("End of the Text File")]
    private int startText;
    private int endText;

    [Header("Button Pressing")]
    public bool buttonPress;
    private bool waitForPress;

    [Header("Triggerzone Gameobjects")]
        //Tooltip("This is used if the Quests are going to find Tags rather than GameObjects")]
//    public QuestTrigger theQuest;
//    private GameObject npc;
//    private GameObject triggerQuest;
//    private GameObject mainQuest;
//    private GameObject mainQuest1;
        [Tooltip("Are you going to destroy the GameObject when the player enters the collider")]
    public bool destroy;

    [Header("Quest Canvas UI")]
        [Tooltip("Text Object for the Quest")]
    public Text questStartedText;
    public Text questEndedText;
        [Tooltip("This will be the Ending/Second Text Object for the Quest")]
    public GameObject questDisappearingText;
        [Tooltip("Text Object for the Quest Information and the yellow select icon")]
    public GameObject questInfo;
    public GameObject questSelectedIcon;

    [Header("Quest Guidance UI")]
     [Tooltip("Gameobject with the animator")]
    public GameObject[] questGuidancePointer;

    [Header("Trigger Zone Game Object")]
        [Tooltip("Main Quest Game Objects")]
    public GameObject[] mainObject;
    public GameObject[] mainObject1;
        [Tooltip("Side Quest Game Objects")]
    public GameObject sideObject;
    public GameObject sideObject1;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        dialogueManager = FindObjectOfType<DialogueQuestManager>();

        //triggerQuest = GameObject.FindGameObjectWithTag("TutQuest");
        //npc = GameObject.FindGameObjectWithTag("TutQuest1");
        //mainQuest = GameObject.FindGameObjectWithTag("sideQuest");
        //mainQuest1 = GameObject.FindGameObjectWithTag("sideQuest1");
			//Used these when searching for the gameObjects tags rather than manually attaching the gameObjects - currently obselete
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Original Format - This was used when it all just one quest and divided up into Main and Side
            //if (other.gameObject.name == "Player")
            //{
            //    if (isMainQuest == true)
            //    {
            //            StartQuest();
            //    }

		//We need to add another if statement for the "Old Player" (TO DO LIST!!!)

        if (isMainQuest == true)
			//Is this a Main Quest?
        {
            if (other.gameObject.name == "Player")
            {
                StartQuest();
            }

            //if (other.gameObject.name == "Old Player")
            //{
            //    StartQuest();
            //}

            else if (other.gameObject.tag == "Roaming Enemies")
            {
                return; 
            }

            else if (other.gameObject.tag == "NPC" && other.gameObject.tag == "Shopkeeper")
            {
                return;
            }
            
            if (!questManager.questsCompleted[questNumber])
            {
                if (startQuest && !questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].gameObject.SetActive(true);
                    questManager.quests[questNumber].StartQuest();

                    //questGuidancePointer[0].SetActive(true);

                    //if (!mainObject[0] && questManager.questsCompleted[questNumber] == false)
                    //{
                    //    mainObject[1].SetActive(false);
                    //}

                    //if (!mainObject[1] && questManager.questsCompleted[questNumber] == false)
                    //{
                    //    mainObject[2].SetActive(false);
                    //    print("quest 2 not active");
                    //}
                }

                if (endQuest && questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].EndQuest();
                    StartCoroutine(EndedQuest());
                    //This will count 4 seconds before disabling the quest completed text

                    if (questManager.questsCompleted[0] == true)
                    {
                        mainObject[1].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(true);
                    }

                    if (questManager.questsCompleted[1] == true)
                    {
                        mainObject[2].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(true);
                    }

                    if (questManager.questsCompleted[2] == true)
                    {
                        mainObject[3].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(false);
                        questGuidancePointer[3].SetActive(true);
                    }

                    if (questManager.questsCompleted[3] == true)
                    {
                        mainObject[4].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(false);
                        questGuidancePointer[3].SetActive(false);
                        questGuidancePointer[4].SetActive(true);
                    }

                    if (questManager.questsCompleted[4] == true)
                    {
                        mainObject[5].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(false);
                        questGuidancePointer[3].SetActive(false);
                        questGuidancePointer[4].SetActive(false);
                        questGuidancePointer[5].SetActive(true);
                    }

                    if (questManager.questsCompleted[5] == true)
                    {
                        mainObject[6].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(false);
                        questGuidancePointer[3].SetActive(false);
                        questGuidancePointer[4].SetActive(false);
                        questGuidancePointer[5].SetActive(false);
                        questGuidancePointer[6].SetActive(true);
                    }

                    if (questManager.questsCompleted[6] == true)
                    {
                        mainObject[7].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(false);
                        questGuidancePointer[3].SetActive(false);
                        questGuidancePointer[4].SetActive(false);
                        questGuidancePointer[5].SetActive(false);
                        questGuidancePointer[6].SetActive(false);
                        questGuidancePointer[7].SetActive(true);
                    }

                    if (questManager.questsCompleted[7] == true)
                    {
                        mainObject[8].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(false);
                        questGuidancePointer[3].SetActive(false);
                        questGuidancePointer[4].SetActive(false);
                        questGuidancePointer[5].SetActive(false);
                        questGuidancePointer[6].SetActive(false);
                        questGuidancePointer[7].SetActive(false);
                        questGuidancePointer[8].SetActive(true);
                    }

                    if (questManager.questsCompleted[8] == true)
                    {
                        mainObject[9].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(false);
                        questGuidancePointer[3].SetActive(false);
                        questGuidancePointer[4].SetActive(false);
                        questGuidancePointer[5].SetActive(false);
                        questGuidancePointer[6].SetActive(false);
                        questGuidancePointer[7].SetActive(false);
                        questGuidancePointer[8].SetActive(false);
                        questGuidancePointer[9].SetActive(true);
                    }

                    if (questManager.questsCompleted[9] == true)
                    {
                        mainObject[10].SetActive(true);
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(false);
                        questGuidancePointer[3].SetActive(false);
                        questGuidancePointer[4].SetActive(false);
                        questGuidancePointer[5].SetActive(false);
                        questGuidancePointer[6].SetActive(false);
                        questGuidancePointer[7].SetActive(false);
                        questGuidancePointer[8].SetActive(false);
                        questGuidancePointer[9].SetActive(false);
                        questGuidancePointer[10].SetActive(true);
                    }

                    if (questManager.questsCompleted[10] == true)
                    {
                        questGuidancePointer[0].SetActive(false);
                        questGuidancePointer[1].SetActive(false);
                        questGuidancePointer[2].SetActive(false);
                        questGuidancePointer[3].SetActive(false);
                        questGuidancePointer[4].SetActive(false);
                        questGuidancePointer[5].SetActive(false);
                        questGuidancePointer[6].SetActive(false);
                        questGuidancePointer[7].SetActive(false);
                        questGuidancePointer[8].SetActive(false);
                        questGuidancePointer[9].SetActive(false);
                        questGuidancePointer[10].SetActive(false);
                    }

                    //if(mainObject[0] && questManager.questsCompleted[questNumber] == true)
                    //{
                    //    mainObject[1].SetActive(true);
                    //}

                    //if (mainObject[1] && questManager.questsCompleted[questNumber] == true)
                    //{
                    //    mainObject[2].SetActive(true);
                    //    print("quest 2 activated");
                    //}

                    //triggerQuest.SetActive(false);
                    //npc.SetActive(false);
                    //questObject.GetComponent<Collider2D>().enabled = false;
                    //questObject1.GetComponent<Collider2D>().enabled = false;
                    //These are used when searching for the tags to disable the 2D Box Collider components
                }
            }
        }

        if (isSideQuest == true)
			//Is this a Side Quest?
        {
            if (other.gameObject.name == "Player")
            {
                    SideQuestStart();
            }

            if (!questManager.questsCompleted[questNumber])
            {

                if (startQuest && !questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].gameObject.SetActive(true);
                    questManager.quests[questNumber].SideQuestStart();
                }

                if (endQuest && questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].SideQuestEnd();
                    StartCoroutine(EndedQuest());
						//This will count 4 seconds before disabling the quest completed text
                }
            }
        }
    }

    //MAIN QUEST!!!
    public void StartQuest()
    {
        dialogueManager.ReloadScript(theText);
        dialogueManager.currentLine = startLine;
        dialogueManager.endLine = endLine;
        dialogueManager.EnableDialogueBox();
        dialogueManager.nameBoxText.text = "" + nameText;
        //These will call upon the Dialogue Manager script - Reloads the Text File so that it continue when it reaches the end line.

        mainObject[0].GetComponent<Collider2D>().enabled = false;
        mainObject1[0].SetActive(true);
        //Main quest gameObjects
        //When the player enters the starting trigger it will disable the trigger component - starting quest dialogue won't appear a second time
        //Activates the ending quest trigger so that the player can finish the quest

        questSelectedIcon.SetActive(true);
        questStartedText.text = "Quest: " + questNumber;
        //Quest selection icon appears
        //Will display the quest number (you need to specify what quest number it is for both the starting and ending gameObjects)
        questGuidancePointer[0].SetActive(true);

        questInfo.SetActive(true);
        //Enables the quest info - information about the selected quest

                //if (questManager.quests[1].gameObject.activeSelf == true)
                //{
                //    questGuidancePointer[0].SetActive(false);
                //    questGuidancePointer[1].SetActive(true);
                //}
    }

    public void EndQuest()
    {
        //dialogueManager.DisableDialogueBox();
        dialogueManager.EnableDialogueBox();
        dialogueManager.currentLine = startText;
        dialogueManager.endLine = endText;
        dialogueManager.nameBoxText.text = "" + nameText;

        questManager.questsCompleted[questNumber] = true;
        //When entering the ending quest trigger it will notify the Quest Manager - checking and ticking the quest completed box

        mainObject1[0].GetComponent<Collider2D>().enabled = false;
        questSelectedIcon.SetActive(false);

        questStartedText.enabled = false;
        questEndedText.text = "Quest: " + questNumber + " Completed!";
        questInfo.SetActive(false);
    }

    IEnumerator EndedQuest()
    {
        yield return new WaitForSeconds(4);
        questDisappearingText.SetActive(false);
			//This will count 4 seconds before disabling the quest completed text
    }

                                //SIDE QUEST!!!
    public void SideQuestStart()
    {
        dialogueManager.ReloadScript(theText);
        dialogueManager.currentLine = startLine;
        dialogueManager.endLine = endLine;
        dialogueManager.EnableDialogueBox();
        dialogueManager.nameBoxText.text = "" + nameText;
        //These will call upon the Dialogue Manager script - Reloads the Text File so that it continue when it reaches the end line.

        sideObject.GetComponent<Collider2D>().enabled = false;
        sideObject1.SetActive(true);
			//Side quest gameObjects
			//When the player enters the starting trigger it will disable the trigger component - starting quest dialogue won't appear a second time
			//Activates the ending quest trigger so that the player can finish the quest

        questSelectedIcon.SetActive(true);
        questStartedText.text = "Quest: " + questNumber;
        //Quest selection icon appears
        //Will display the quest number (you need to specify what quest number it is for both the starting and ending gameObjects)

        questInfo.SetActive(true);
			//Enables the quest info - information about the selected quest
    }

    public void SideQuestEnd()
    {
		//dialogueManager.DisableDialogueBox();
		dialogueManager.EnableDialogueBox();
        dialogueManager.currentLine = startText;
        dialogueManager.endLine = endText;
        dialogueManager.nameBoxText.text = "" + nameText;

        questManager.questsCompleted[questNumber] = true;
        sideObject1.GetComponent<Collider2D>().enabled = false;
        questSelectedIcon.SetActive(false);

        questStartedText.enabled = false;
        questEndedText.text = "Quest: " + questNumber + " Completed!";
        questInfo.SetActive(false);
    }
}
