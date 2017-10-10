using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OLDQuestTrigger : MonoBehaviour
{
    //[Header("Calling on the Quest and Dialogue Manager Scripts")]
    //private QuestManager questManager;
    //private DialogueBoxManager dialogueManager;

    //[Header("What Quest Number it is")]
    //public int questNumber;

    //public bool startQuest;
    //public bool endQuest;

    //public bool isSideQuest;
    //public bool isTutQuest;

    //[Header("Text File")]
    //public TextAsset theText;

    //[Header("Start of the Text File")]
    //public int startLine;
    //public int endLine;

    //[Header("End of the Text File")]
    //public int startText;
    //public int endText;

    //[Header("Button Pressing")]
    //public bool buttonPress;
    //private bool waitForPress;

    //[Header("Triggerzone Gameobjects")]
    //public QuestTrigger theQuest;
    //private GameObject npc;
    //private GameObject triggerQuest;
    //private GameObject mainQuest;
    //private GameObject mainQuest1;
    //public bool destroy;

    //[Header("Quest Canvas UI")]
    //public Text questStartedText;
    //public Text questEndedText;
    //public GameObject questDisappearingText;
    //public Text questInfo;
    //public GameObject questSelectedIcon;

    //void Start()
    //{
    //    questManager = FindObjectOfType<QuestManager>();
    //    dialogueManager = FindObjectOfType<DialogueBoxManager>();

    //    triggerQuest = GameObject.FindGameObjectWithTag("TutQuest");
    //    npc = GameObject.FindGameObjectWithTag("TutQuest1");
    //    mainQuest = GameObject.FindGameObjectWithTag("sideQuest");
    //    mainQuest1 = GameObject.FindGameObjectWithTag("sideQuest1");
    //}

    //void Update()
    //{
    //    //if (waitForPress && Input.GetKeyDown(KeyCode.Return))
    //    //{
    //    //    dialogueManager.ReloadScript(theText);
    //    //    dialogueManager.currentLine = startLine;
    //    //    dialogueManager.endLine = endLine;
    //    //    dialogueManager.EnableDialogueBox();
    //    //    //            //charPortrait.gameObject.SetActive(true);
    //    //}
    //    //Only needed if the player is using button pressing to initiate the quest(currently bugged and needs fixing)
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.name == "Player")
    //    {
    //        //if (buttonPress)
    //        //{
    //        //    waitForPress = true;
    //        //    return;
    //        //}
    //        //Not relevant right now
    //        if (isTutQuest == true)
    //        {
    //            StartQuest();

    //        }

    //        if (!questManager.questsCompleted[questNumber])
    //        {
    //            //StartQuest();
    //            if (startQuest && !questManager.quests[questNumber].gameObject.activeSelf)
    //            {
    //                questManager.quests[questNumber].gameObject.SetActive(true);
    //                questManager.quests[questNumber].StartQuest();

    //                //StartQuest ();
    //                //dialogueManager.ReloadScript(theText);
    //                //dialogueManager.currentLine = startLine;
    //                //dialogueManager.endLine = endLine;
    //                //Not relevant right now
    //            }

    //            if (endQuest && questManager.quests[questNumber].gameObject.activeSelf)
    //            {
    //                questManager.quests[questNumber].EndQuest();
    //                triggerQuest.SetActive(false);
    //                npc.SetActive(false);

    //                mainQuest.SetActive(true);
    //                mainQuest1.SetActive(true);

    //                //EndQuest();
    //                //StartCoroutine(EndedQuest());
    //                //dialogueManager.ReloadScript(theText);
    //                //dialogueManager.currentLine = startText;
    //                //dialogueManager.endLine = endText;
    //                //gameObject.SetActive(false);
    //                //gameObject.GetComponent<Collider2D>().enabled = false;
    //                //Not relevant right now
    //            }
    //        }
    //    }
    //}

    //public void StartQuest()
    //{
    //    //if (buttonPress)
    //    //{
    //    //    waitForPress = true;
    //    //    //return;
    //    //}
    //    //questManager.ShowQuestText();
    //    //triggerQuest.GetComponent<Collider2D>().enabled = false;

    //    dialogueManager.ReloadScript(theText);
    //    dialogueManager.currentLine = startLine;
    //    dialogueManager.endLine = endLine;
    //    dialogueManager.EnableDialogueBox();

    //    mainQuest.GetComponent<Collider2D>().enabled = false;
    //    mainQuest1.GetComponent<Collider2D>().enabled = false;
    //    triggerQuest.GetComponent<Collider2D>().enabled = false;
    //    questSelectedIcon.SetActive(true);
    //    questStartedText.text = "Quest: " + questNumber;

    //    if (destroy)
    //    {
    //        Destroy(gameObject);
    //    }
    //    Debug.Log("Quest Has Started");
    //}

    //public void EndQuest()
    //{
    //    //questManager.ShowQuestText();
    //    //dialogueManager.ReloadScript(theText);
    //    //gameObject.GetComponent<Collider2D>().enabled = false;
    //    //theQuest.enabled = false;
    //    //gameObject.SetActive(false);
    //    //npc.SetActive(false);
    //    // npc.GetComponent<Collider2D>().enabled = false;

    //    dialogueManager.EnableDialogueBox();
    //    dialogueManager.DisableDialogueBox();
    //    dialogueManager.currentLine = startText;
    //    dialogueManager.endLine = endText;

    //    questManager.questsCompleted[questNumber] = true;
    //    npc.GetComponent<Collider2D>().enabled = false;
    //    mainQuest.GetComponent<Collider2D>().enabled = true;
    //    mainQuest1.GetComponent<Collider2D>().enabled = true;
    //    questSelectedIcon.SetActive(false);

    //    questStartedText.enabled = false;
    //    questEndedText.text = "Quest: " + questNumber + " Completed!";
    //    questInfo.enabled = false;

    //    if (destroy)
    //    {
    //        Destroy(npc);
    //    }
    //    Debug.Log("Quest Has Ended");
    //}

    ////IEnumerator EndedQuest()
    ////{
    ////    yield return new WaitForSeconds(4);
    ////    //questEndedText.text = " ";
    ////    questDisappearingText.SetActive(false);

    ////    Debug.Log("IS GONE");
    ////}

    ////void OnTriggerExit2D(Collider2D other)
    ////{
    ////    if (other.name == "Player")
    ////    {
    ////        waitForPress = false;
    ////        StartQuest();

    ////    }
    ////    StartQuest();
    ////}

    [Header("Calling on the Quest and Dialogue Manager Scripts")]
    private QuestManager questManager;
    private DialogueBoxManager dialogueManager;

    [Header("What Quest Number it is")]
    public int questNumber;

    public bool startQuest;
    public bool endQuest;

    public bool isSideQuest;
    public bool isTutQuest;

    [Header("Text File")]
    public TextAsset theText;

    [Header("Start of the Text File")]
    public int startLine;
    public int endLine;

    [Header("End of the Text File")]
    public int startText;
    public int endText;

    [Header("Button Pressing")]
    public bool buttonPress;
    private bool waitForPress;

    [Header("Triggerzone Gameobjects")]
    public QuestTrigger theQuest;
    private GameObject npc;
    private GameObject triggerQuest;
    private GameObject mainQuest;
    private GameObject mainQuest1;
    public bool destroy;

    [Header("Quest Canvas UI")]
    public Text questStartedText;
    public Text questEndedText;
    public GameObject questDisappearingText;
    public Text questInfo;
    public GameObject questSelectedIcon;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        dialogueManager = FindObjectOfType<DialogueBoxManager>();

        triggerQuest = GameObject.FindGameObjectWithTag("TutQuest");
        npc = GameObject.FindGameObjectWithTag("TutQuest1");
        mainQuest = GameObject.FindGameObjectWithTag("sideQuest");
        mainQuest1 = GameObject.FindGameObjectWithTag("sideQuest1");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {

            if (isTutQuest == true)
            {
                StartQuest();
            }

            if (!questManager.questsCompleted[questNumber])
            {

                if (startQuest && !questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].gameObject.SetActive(true);
                    questManager.quests[questNumber].StartQuest();
                }

                if (endQuest && questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].EndQuest();
                    triggerQuest.SetActive(false);
                    npc.SetActive(false);

                    mainQuest.SetActive(true);
                    mainQuest1.SetActive(true);
                }
            }
        }
    }

    public void StartQuest()
    {
        dialogueManager.ReloadScript(theText);
        dialogueManager.currentLine = startLine;
        dialogueManager.endLine = endLine;
        dialogueManager.EnableDialogueBox();

        mainQuest.GetComponent<Collider2D>().enabled = false;
        mainQuest1.GetComponent<Collider2D>().enabled = false;
        triggerQuest.GetComponent<Collider2D>().enabled = false;
        questSelectedIcon.SetActive(true);
        questStartedText.text = "Quest: " + questNumber;

        if (destroy)
        {
            Destroy(gameObject);
        }
        Debug.Log("Quest Has Started");
    }

    public void EndQuest()
    {
        dialogueManager.EnableDialogueBox();
        dialogueManager.DisableDialogueBox();
        dialogueManager.currentLine = startText;
        dialogueManager.endLine = endText;

        questManager.questsCompleted[questNumber] = true;
        npc.GetComponent<Collider2D>().enabled = false;
        mainQuest.GetComponent<Collider2D>().enabled = true;
        mainQuest1.GetComponent<Collider2D>().enabled = true;
        questSelectedIcon.SetActive(false);

        questStartedText.enabled = false;
        questEndedText.text = "Quest: " + questNumber + " Completed!";
        questInfo.enabled = false;

        if (destroy)
        {
            Destroy(npc);
        }
        Debug.Log("Quest Has Ended");
    }

    //IEnumerator EndedQuest()
    //{
    //    yield return new WaitForSeconds(4);
    //    //questEndedText.text = " ";
    //    questDisappearingText.SetActive(false);

    //    Debug.Log("IS GONE");
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.name == "Player")
    //    {
    //        waitForPress = false;
    //        StartQuest();

    //    }
    //    StartQuest();
    //}
}
