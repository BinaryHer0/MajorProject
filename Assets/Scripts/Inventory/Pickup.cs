using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public Item item;

    public GameObject inventory;

    private bool itemCollected = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (itemCollected == false)
            {
                Inventory playerInventory = inventory.GetComponent<Inventory>();
                playerInventory.AddItem(item);
                this.gameObject.SetActive(false);
            }
        }
    }
}
