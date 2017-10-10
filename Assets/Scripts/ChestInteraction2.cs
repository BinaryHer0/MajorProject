using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestInteraction2 : MonoBehaviour
{
	[Header("Calling on the Activate Dialogue and Dialogue Box Manager")]
		[Tooltip("You can attach the Activate Dialogue script to the gameObject and then drag that into the chestCanTalk")]
    public ActivateDialogue chestCanTalk;
    public DialogueBoxManager dialogueManager;

	[Header("Sprite Assets and Item List")]
		[Tooltip("Closed Sprite")]
    public Sprite sprite1; // Drag your first sprite here
		[Tooltip("Open Sprite")]
    public Sprite sprite2; // Drag your second sprite here
		[Tooltip("Item List/Info for the Chest")]
    public Item chestItem;

	[Header("Is the Chest Open? & Gold Accumulation")]
	private bool chestOpened = false;
	public int goldAmount;

	[Header("Does it require a button press?")]
    public bool buttonPress;
    private bool waitForPress;

	[Header("Inventory GameObject")]
    private SpriteRenderer spriteRenderer;
    public GameObject inventoryObject;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
			// we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) 
			// if the sprite on spriteRenderer is null then
        {
            spriteRenderer.sprite = sprite1; 
			// set the sprite to sprite1
        }

        //dialogueManager = FindObjectOfType<DialogueBoxManager>();
        chestCanTalk = GetComponent<ActivateDialogue>();
        chestCanTalk.enabled = true;
    }

    void Update()
    {
		if (waitForPress && (Input.GetKeyDown(KeyCode.E))) 
				// If E is pushed down or Enter
        {
            ChangeTheDamnSprite(); 
				// call method to change sprite
        }

        //if (waitForPress && (Input.GetKeyDown(KeyCode.Return)))
        //// If E is pushed down or Enter
        //{
        //    ChangeTheDamnSprite();
        //    // call method to change sprite
        //}
    }

    void ChangeTheDamnSprite()
    {
        if (spriteRenderer.sprite == sprite1) 
			// if the spriteRenderer sprite = sprite1 then change to sprite2
        {
            spriteRenderer.sprite = sprite2;
            if (chestOpened == false)
            {
                Inventory inventory = inventoryObject.GetComponent<Inventory>();
                inventory.AddItem(chestItem);
                print("Adding " + chestItem + " AddItem");
                chestOpened = true;
            }
        }

        else
        {
            spriteRenderer.sprite = sprite1; 
				// otherwise change it back to sprite1
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (buttonPress)
            {
                waitForPress = true;
                return;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            waitForPress = false;
        }
    }
}