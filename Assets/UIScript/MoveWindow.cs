using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveWindow : MonoBehaviour,IDragHandler
{

    [SerializeField] private RectTransform dragRecTransform;
    [SerializeField] private Canvas _canvas;


    public void OnDrag(PointerEventData eventData)
    {
        dragRecTransform.anchoredPosition += eventData.delta /3/ (_canvas.scaleFactor)/3;
    }
}
