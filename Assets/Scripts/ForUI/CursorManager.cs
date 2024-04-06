using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorAim;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        SetAimCursor();
    }
    public void SetAimCursor()
    {
        Cursor.SetCursor(cursorAim, hotSpot, CursorMode.Auto);
    }
}