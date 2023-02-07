using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveWindow : MonoBehaviour,IDragHandler
{

    [SerializeField] private RectTransform dragRecTransform;
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        dragRecTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRecTransform.anchoredPosition += eventData.delta/(_canvas.scaleFactor);
    }
}
