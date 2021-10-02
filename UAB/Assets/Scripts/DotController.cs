using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DotController : MonoBehaviour , IDragHandler , IPointerClickHandler
{
    [HideInInspector] public LineController line;
    [HideInInspector] public int index;
    public Action<DotController> OnDragEvent;
    [HideInInspector] public Image image;


    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this);
    }

    public Action<DotController> OnClickEvent;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }

    public void SetLine(LineController line)
    {
        this.line = line;
    }
}
