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
    [SerializeField] private GameObject HubPrefab;
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
    private MoneyManager moneyManager;

    private void Awake()
    {
        cam = Camera.main;
        penCanvas.OnPenCanvasLeftClickEvent += AddDot;
        moneyManager = FindObjectOfType<MoneyManager>();
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

        //Creating a hub
        else if (ModeController.Instance.currentMode == Modes.HubCreating)
        {
            
            //Create a new line no matter what
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<LineController>();
            currentLine.penTool = this;
            
            DotController dot = Instantiate(HubPrefab, GetMousePosition(), Quaternion.identity, dotParent).GetComponent<DotController>();
            dot.index = 0;
            dot.OnDragEvent += MoveDot;
            dot.OnClickEvent += DotClick;
            dot.SetLine(currentLine);
            LastSelectedDot = null;
            dot.index = currentLine.AddPoint(dot, LastSelectedDot);
            LastSelectedDot = dot;
            startingDot = dot;
            moneyManager.BuildHub();
            ModeController.Instance.SwitchToCreate();
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
        if (currentLine != null)
        {
            foreach (var item in currentLine.points)
            {
                item.isEditable = false;
                item.OnDragEvent -= MoveDot;
                item.OnClickEvent -= DotClick;
            }
            float lastLenght = currentLine.lenght;
            Debug.Log(lastLenght);
            currentLine = null;
            lastSelectedDot = null;
            AddExistingDot(startingDot);
            moneyManager.BuildLine(lastLenght);
        }
    }
    private void DotClick(DotController dot)
    {
        LastSelectedDot = dot;
        if (dot.isHub)
        {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<LineController>();
            currentLine.penTool = this;

            dot.index = 0;
            dot.OnDragEvent += MoveDot;
            dot.OnClickEvent += DotClick;
            dot.SetLine(currentLine);
            LastSelectedDot = null;
            LastSelectedDot = dot;
            dot.index = currentLine.AddPoint(dot, LastSelectedDot);
            
            startingDot = dot;
            ModeController.Instance.SwitchToCreate();
        }
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
