using UnityEngine;
using UnityEngine.Networking;

public class CursorState : NetworkBehaviour
{

    public static bool isCursorLocked = true;

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            //CheckForInput();
            CheckIfCursorShouldBeLocked();
        }
    }

    void ToogleCursorState()
    {
        if (PauseMenu.IsOn)
        {
            isCursorLocked = false;
        }
        else
        {
            isCursorLocked = true;
        }
    }

    void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToogleCursorState();
        }
    }

    void CheckIfCursorShouldBeLocked()
    {
        if (PauseMenu.IsOn)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!PauseMenu.IsOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

