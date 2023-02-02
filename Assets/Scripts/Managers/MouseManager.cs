using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public void Visible()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void InVisible()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
