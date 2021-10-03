using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;

    public static bool isPaused;
    private LevelLoader levelLoader;
    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }
    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            ModeController.Instance.SwitchToMove();
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            ModeController.Instance.SwitchToPaused();
        }

    }

    public void LoadMenu()
    {
        Resume();
        levelLoader.LoadLevel(0);
    }
}
