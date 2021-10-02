using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenTool : MonoBehaviour
{
    [Header("Dots")]
    [SerializeField] GameObject dotPrefab;
    [SerializeField] private Transform dotParent;

    [Header("Lines")]
    [SerializeField] private Transform lineParent;
    [SerializeField] GameObject linePrefab;

    private LineController currentLine;
    private Camera cam;

    private int touchCount;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Touched");
            if (currentLine == null)
            {
                currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<LineController>();
            }
            GameObject dot = Instantiate(dotPrefab, GetTouchPosition(), Quaternion.identity, dotParent);
            currentLine.AddPoint(dot.transform);
        }
    }

    private Vector3 GetTouchPosition()
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}
