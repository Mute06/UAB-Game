using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNetChecker : MonoBehaviour
{
    public bool isLevelCompleted = false;
    public int totalBuildings;
    public GameObject CompletePanel;
    private int totalConnectedBuildings;
    public int TotalConnectedBuildings
    {
        get
        {
            return totalConnectedBuildings;
        }
        set
        {
            totalConnectedBuildings = value;
            if (totalConnectedBuildings >= totalBuildings)
            {
                //Level Completed
                Debug.Log("Level Completed");
                LevelCompleted();
            }
        }
    }

    private void LevelCompleted()
    {
        if (!isLevelCompleted)
        {
            isLevelCompleted = true;
            CompletePanel.SetActive(true);
        }
    }

}
