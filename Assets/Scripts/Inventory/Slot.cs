using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour {

    public Item slotItem;
    public GameObject slotObject;
    private Inventory inventory;

    void Awake() // Sets this slots object to itself and finds inventory
    {
        slotObject = this.gameObject;
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
           
    }

    public void UpdateSlotItems() 
    {
        for (int i = 0; i < inventory.itemSlots.Length; i++) // for this slot, checks through the inventory items array and sets the slot item to item in array
        {
            if (this.gameObject == inventory.itemSlots[i])
            { 
                slotItem = inventory.items[i];
                print(this.gameObject.name + " has " + slotItem + " in its slot");
            }

        }
    }

    public void TooltipInfoGathered()
    {
        if (slotItem == null && inventory.movingItem == false)
        {
            inventory.EnableInfoPanel();
        }

        else if (inventory.movingItem == true)
        {
            inventory.itemToReplace = slotItem; // itemToReplace becomes whats currently in the slot
            
            for (int i = 0; i < inventory.itemSlots.Length; i++)
            {
                if (this.gameObject == inventory.itemSlots[i])
                {
                    inventory.items[i] = inventory.itemToMove;
                    print(this.gameObject.name + " has " + slotItem + " in its slot");
                    inventory.itemImages[i].sprite = inventory.itemToMove.sprite;
                    inventory.itemImages[i].enabled = true;
                    inventory.itemSlots[i].GetComponent<Slot>().UpdateSlotItems();
                    inventory.EnableInfoPanel(slotItem);
                    inventory.ChangingItem();
                }
            } 

            inventory.movingItem = false;
        }

        else
        {
            print(slotItem);
            inventory.itemSlotToReplace = this.gameObject;
            inventory.EnableInfoPanel(slotItem);
        }
    }

    public void TooltipInfoDisabled()
    {
        print(this.gameObject.name + " Mouse Exit");
        inventory.DisableInfoPanel();
    }




}
