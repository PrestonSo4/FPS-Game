using UnityEngine;
using UnityEngine.Networking;

public class CursorState : NetworkBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            //CheckForInput();
            CheckIfCursorShouldBeLocked();
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

