using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPanel : MonoBehaviour
{
    public void DisablePanel()
    {
        gameObject.SetActive(false);
        ModeController.Instance.SwitchToMove();
    }
    public void ActiveObject(GameObject panel)
    {
        panel.SetActive(true);
        ModeController.Instance.SwitchToPaused();
    }
}
