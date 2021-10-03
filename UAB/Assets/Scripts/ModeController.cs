using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    #region Singleton
    private static ModeController _instance;
    public static ModeController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Mode Controller");
                go.AddComponent<ModeController>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion
    public Modes currentMode = Modes.Moving;
    public GameObject CreateButton;
    public GameObject completeButton;
    public PenTool penTool;
    public void SwitchToMove()
    {
        CreateButton.SetActive(true);
        completeButton.SetActive(false);
        currentMode = Modes.Moving;
    }

    public void SwitchToCreate()
    {
        currentMode = Modes.Creating;
        CreateButton.SetActive(false);
        completeButton.SetActive(true);
    }

    public void SwitchToHubCreation()
    {
        currentMode = Modes.HubCreating;
        CreateButton.SetActive(true);
        completeButton.SetActive(false);
    }

    public void CompleteDrawingLine()
    {
        penTool.StopEditingCurrentLine();
    }
}



public enum Modes
{
    Moving,
    Creating,
    HubCreating
}
