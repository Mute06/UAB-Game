using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenTool : MonoBehaviour
{
    [Header("Pen Canvas")]
    [SerializeField] private PenCanvas penCanvas;

    [Header("Dots")]
    [SerializeField] GameObject dotPrefab;
    [SerializeField] private Transform dotParent;
    [SerializeField] private DotController startingDot;
    private DotController lastSelectedDot;
    public DotController LastSelectedDot { 
        get
        {
            return lastSelectedDot;
        }
        set
        {
            if (lastSelectedDot != null)
            {
                lastSelectedDot.image.color = NormalDotColor;
            }
            lastSelectedDot = value;

            if (lastSelectedDot != null)
            {
                lastSelectedDot.image.color = SelectedDotColor;
            }
        }
    }

    [Header("Lines")]
    [SerializeField] private Transform lineParent;
    [SerializeField] GameObject linePrefab;

    [Header("Colors")]
    [SerializeField] Color NormalDotColor;
    [SerializeField] Color SelectedDotColor;

    private LineController currentLine;
    public LineController CurrentLine
    {
        get
        {
            return currentLine;
        }
    }

    private Camera cam;


    private void Awake()
    {
        cam = Camera.main;
        penCanvas.OnPenCanvasLeftClickEvent += AddDot;
    }

    private void Start()
    {
        if (startingDot != null)
        {
            AddExistingDot(startingDot);
        }
    }

    private void AddDot()
    {
        if (ModeController.Instance.currentMode == Modes.Creating)
        {
            Debug.Log("Touched");
            if (currentLine == null)
            {
                currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<LineController>();

                currentLine.penTool = this;
            }
            DotController dot = Instantiate(dotPrefab, GetMousePosition(), Quaternion.identity, dotParent).GetComponent<DotController>();
            dot.OnDragEvent += MoveDot;
            dot.OnClickEvent += DotClick;
            dot.SetLine(currentLine);
            dot.index = currentLine.AddPoint(dot, LastSelectedDot);
            LastSelectedDot = dot;
        }

    }

    public void AddExistingDot(DotController dot)
    {
        if (currentLine == null)
        {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<LineController>();

            currentLine.penTool = this;
        }
        dot.OnDragEvent += MoveDot;
        dot.OnClickEvent += DotClick;
        dot.SetLine(currentLine);
        dot.index = currentLine.AddPoint(dot, LastSelectedDot);
        LastSelectedDot = dot;
    }

    public void StopEditingCurrentLine()
    {
        foreach (var item in currentLine.points)
        {
            //item.OnDragEvent -= MoveDot;
            //item.OnClickEvent -= DotClick;
            item.isEditable = false;
        }
        currentLine = null;
        lastSelectedDot = null;
    }
    private void DotClick(DotController dot)
    {
        LastSelectedDot = dot;
    }

    private void MoveDot(DotController dot)
    {
        LastSelectedDot = dot;
        dot.transform.position = GetMousePosition();
    }

    private Vector3 GetMousePosition()
    {

        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;

    }

    private RaycastHit2D RaycastMousePosition()
    {
        Vector2 raycastPos = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

        return hit;
    }
}
