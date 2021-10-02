using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private List<DotController> points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
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

    /*
    public void SplitPointsAtIndex(int index, out List<DotController> beforeDots, out List<DotController> afterDots)
    {

    }
    */

    private void LateUpdate()
    {
        if (lr.positionCount >= 2)
        {
            for (int i = 0; i < points.Count; i++)
            {
                lr.SetPosition(i, points[i].transform.position);
            }
        }
    }
}
