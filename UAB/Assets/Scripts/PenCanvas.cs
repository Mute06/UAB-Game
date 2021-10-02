using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PenCanvas : MonoBehaviour, IPointerClickHandler
{
    public Action OnPenCanvasLeftClickEvent;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPenCanvasLeftClickEvent?.Invoke();
    }
}
