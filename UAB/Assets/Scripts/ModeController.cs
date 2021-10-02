using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SwitchToMove()
    {
        currentMode = Modes.Moving;
    }

    public void SwitchToCreate()
    {
        currentMode = Modes.Creating;
    }
}



public enum Modes
{
    Moving,
    Creating
}
