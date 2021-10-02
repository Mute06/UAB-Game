using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public PenTool penTool;
    public float lenght;
    private LineRenderer lr;
    public List<DotController> points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
        lr.sortingLayerName = "Dots";
        points = new List<DotController>();
    }
    /// <summary>
    /// Adds a point to line
    /// </summary>
    /// <param name="point"> </param>
    /// <param name="lastSelectedPoint"></param>
    /// <returns>
    /// index of the added point
    /// </returns>
    public int AddPoint(DotController point, DotController lastSelectedPoint)
    {
        lr.positionCount++;

        if (lastSelectedPoint != null)
        {
            int index = lastSelectedPoint.index + 1;
            points.Insert(index, point);
            return index;
        }
        else
        {
            points.Add(point);
            return points.Count - 1;
        }
    }

    public void SetUpLine(List<DotController> points)
    {
        lr.positionCount = points.Count;
        this.points = points;
    }


    private void LateUpdate()
    {
        if (penTool.CurrentLine == this)
        {
            if (lr.positionCount >= 2)
            {
                float _lenght = 0f;
                for (int i = 0; i < points.Count; i++)
                {
                    if (i - 1 >= 0)
                    {
                        _lenght += Vector2.Distance(points[i - 1].transform.position, points[i].transform.position);
                    }
                    lr.SetPosition(i, points[i].transform.position);
                }
                lenght = _lenght;
            }
        }
    }

}
