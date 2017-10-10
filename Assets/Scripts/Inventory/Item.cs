 using UnityEngine;


[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{

    // Item Information
    [Header("Item Information")]
    public string itemName;
    public string itemDescription;
    public int itemID;
    public Sprite sprite;
    public int itemValue;
    public int itemStack;
    public bool consumable = false;

    // What the Item effects
    [Header("Item Stats")]
    public int stamina; 
    public int agility;
    public int defence;
    public int health;
    public int attack;

}
