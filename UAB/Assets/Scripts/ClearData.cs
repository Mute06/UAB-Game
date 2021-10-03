using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearData : MonoBehaviour
{
    const string prefs_achivedLevel = "achivedLevel";
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey(prefs_achivedLevel);
    }
}
