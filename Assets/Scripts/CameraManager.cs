using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera camera_2;
    [SerializeField] private float moveSpeed;

    [SerializeField] private int zoomMax;
    [SerializeField] private int zoomMin;

    private Vector3 midClickPos;
    private Vector3 midClickDrag;

    private void LateUpdate()
    {
        KeyboardInputs();
        MouseInputs();
    }

    private void KeyboardInputs()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
    }

    private void MouseInputs()
    {
        // Middle click drag movement
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            midClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            midClickDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            Camera.main.transform.position = midClickPos - midClickDrag;
        }

        // Zoom in/out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ZoomOut();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ZoomIn();
        }
    }

    public void ZoomIn()
    {
        float size = Mathf.Clamp(Camera.main.orthographicSize - 1, zoomMin, zoomMax);
        Camera.main.orthographicSize = size;
        camera_2.orthographicSize = size;
    }

    public void ZoomOut()
    {
        float size = Mathf.Clamp(Camera.main.orthographicSize + 1, zoomMin, zoomMax);
        Camera.main.orthographicSize = size;
        camera_2.orthographicSize = size;
    }

    public void MoveUp()
    {
        var move = new Vector3(moveSpeed, 0, moveSpeed);
        transform.position += move * Camera.main.orthographicSize / zoomMax;
    }

    public void MoveDown()
    {
        var move = new Vector3(-moveSpeed, 0, -moveSpeed);
        transform.position += move * Camera.main.orthographicSize / zoomMax;
    }

    public void MoveRight()
    {
        transform.position += transform.right * moveSpeed * Camera.main.orthographicSize / zoomMax;
    }

    public void MoveLeft()
    {
        transform.position += transform.right * -moveSpeed * Camera.main.orthographicSize / zoomMax;
    }
}
