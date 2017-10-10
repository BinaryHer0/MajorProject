using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverIcon : MonoBehaviour
{
    [Header("Cursor Texture: Mouse")]
    public Texture2D cursor;

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        //When the mouse enters the object with the script attached to it, it will change to the set cursor image.
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        //When the mouse exits the object it will return to the default cursor image.
    }
}