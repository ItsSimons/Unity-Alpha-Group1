using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StuckInScreen : MonoBehaviour,IDragHandler
{
    private RectTransform _rectTransform;
    public int boarderSnapSize = 10; 
    
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = (RectTransform)transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        trapToScreen();
    }
    private void trapToScreen()
    {
        Vector3 diffMin = _rectTransform.position + (Vector3)_rectTransform.rect.position;
        Vector3 diffMax = (Vector3)Camera.main.pixelRect.size - _rectTransform.position +
                          (Vector3)_rectTransform.rect.position;

        if (diffMin.x < boarderSnapSize)
        {
            _rectTransform.position -= new Vector3(diffMin.x, 0f, 0f);
        }
        
        if (diffMin.y < boarderSnapSize)
        {
            _rectTransform.position -= new Vector3(0f, diffMin.y, 0f);
        }
        
        if (diffMax.x<boarderSnapSize)
        {
            _rectTransform.position += new Vector3(diffMax.x,0f,0f);
        }
        
        if (diffMax.y<boarderSnapSize)
        {
            _rectTransform.position += new Vector3(0f,diffMax.y,0f);
        }
        

    }
}
