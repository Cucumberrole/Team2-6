using UnityEngine;

public class HideMouseCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }
}