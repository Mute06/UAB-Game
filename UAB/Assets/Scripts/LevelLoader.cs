using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    const int levelSelectionSceneIndex = 0;
    const string prefs_achivedLevel = "achivedLevel";
    public float transitionTime = 1f;
    public Animator transition;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(prefs_achivedLevel))
        {
            PlayerPrefs.SetInt(prefs_achivedLevel, 0);
        }
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCountInBuildSettings - 1)
        {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        }

    }

    public void LoadLevel(int sceneIndex)
    {
        if (sceneIndex - 1  <= PlayerPrefs.GetInt(prefs_achivedLevel))
        {
            StartCoroutine(LoadScene(sceneIndex));
        }
        else
        {
            Debug.Log("Level hasn't achived yet");
        }
    }

    public void CompleteCurrentLevel()
    {
        PlayerPrefs.SetInt(prefs_achivedLevel, SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(LoadScene(levelSelectionSceneIndex));

    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
