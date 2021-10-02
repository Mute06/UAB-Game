using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForDots : MonoBehaviour
{
    public CityNetChecker cityNetChecker;

    public float checkRadius;
    public LayerMask DotsLayer;
    private bool didCounted = false;

    private void Start()
    {
        if (cityNetChecker == null)
        {
            cityNetChecker = FindObjectOfType<CityNetChecker>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool result = Physics2D.OverlapCircle(transform.position, checkRadius, DotsLayer);
        //If connection wents out
        if (didCounted && !result)
        {
            didCounted = false;
            cityNetChecker.TotalConnectedBuildings -= 1;
        }
        if (!didCounted && result)
        {
            didCounted = true;
            cityNetChecker.TotalConnectedBuildings += 1;
        }
    }
}
