using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBookControl : MonoBehaviour
{
    public DesktopMovement player;

    public void Close()
    {
        player.guideIsOn = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
