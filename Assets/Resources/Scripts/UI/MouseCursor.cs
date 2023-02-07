using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D objectCursor;

    protected virtual void OnMouseEnter()
    {
        Cursor.SetCursor(objectCursor, Vector2.zero, CursorMode.Auto);
    }
    protected virtual void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
