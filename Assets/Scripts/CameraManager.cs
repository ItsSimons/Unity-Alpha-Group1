using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    [SerializeField] private int zoomMax;
    [SerializeField] private int zoomMin;

    private Vector3 midClickPos;
    private Vector3 midClickDrag;

    private void LateUpdate()
    {
        HandleInput();
    }

    /// <summary>
    /// Inputs for camera movement
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            var move = new Vector3(moveSpeed, 0, moveSpeed);
            transform.position += move * Camera.main.orthographicSize / zoomMax;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            var move = new Vector3(-moveSpeed, 0, -moveSpeed);
            transform.position += move * Camera.main.orthographicSize / zoomMax;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * moveSpeed * Camera.main.orthographicSize / zoomMax;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += transform.right * -moveSpeed * Camera.main.orthographicSize / zoomMax;
        }

        //zoom in and out with middle mouse scroll
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + 1, zoomMin, zoomMax);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - 1, zoomMin, zoomMax);
        }

        //camera movement by clicking middle mouse button down
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            midClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            midClickDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            Camera.main.transform.position = midClickPos - midClickDrag;
        }
    }
}
