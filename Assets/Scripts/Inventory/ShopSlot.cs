using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public Item slotItem;
    public GameObject slotObject;
    public GameObject shopInventoryObject;
    private ShopInventory shopInventory;

    void Awake() // Sets this slots object to itself and finds inventory
    {
        slotObject = this.gameObject;

        shopInventory = shopInventoryObject.GetComponent<ShopInventory>();

    }

    public void UpdateSlotshopItems()
    {
        for (int i = 0; i < shopInventory.shopItemSlots.Length; i++) // for this slot, checks through the inventory shopItems array and sets the slot item to item in array
        {
            if (this.gameObject == shopInventory.shopItemSlots[i])
            {
                
                    slotItem = shopInventory.shopItems[i];
                    print(this.gameObject.name + " has " + slotItem + " in its slot");
                    return; 
                
                
            }

        }

    }

    public void TooltipInfoGathered()
    {
        if (slotItem == null)
        {
            shopInventory.EnableInfoPanel();
        }

        else
        {
            print(slotItem);
            shopInventory.EnableInfoPanel(slotItem);
        }
    }

    public void TooltipInfoDisabled()
    {
        print(this.gameObject.name + " Mouse Exit");
        shopInventory.DisableInfoPanel();
    }




}
