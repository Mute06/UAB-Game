using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lr_Testing : MonoBehaviour
{
    [SerializeField] private List<DotController> points;
    [SerializeField] private LineController line;

    private void Start()
    {
        line.SetUpLine(points);
    }
}
