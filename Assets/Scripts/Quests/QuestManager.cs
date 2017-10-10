using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Calling on the Dialogue Manager Script")]
    public DialogueQuestManager dialogueManager;

    [Header("What Quest Number it is")]
		[Tooltip ("You need to drag in the starting Quest gameObject into this array")]
    public QuestTrigger[] quests;

    [Header("Yes/No - Quest Completed")]
		[Tooltip ("This will automatically tick and update itself if a quest has ended and been completed")]
    public bool[] questsCompleted;

    void Start ()
    {
        questsCompleted = new bool[quests.Length];
        	//New array the same length as the quests
	}
	
    public void ShowQuestText()
    {
        dialogueManager.EnableDialogueBox();
        dialogueManager.currentLine = 0;
    }
}