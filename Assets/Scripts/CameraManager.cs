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

    private void HandleInput()
    {
        /// <summary>
        /// Inputs for camera movement
        /// </summary>

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            var move = new Vector3(moveSpeed, 0, moveSpeed);
            transform.position +=  move * Camera.main.orthographicSize / zoomMax;
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

    /// <summary>
    /// Allows camera movement by dragging mouse with middle click
    /// </summary>
   
    /*void OnGUI()
    {
        GUI.Box(Rect((Screen.width / 2) - 140, 5, 280, 25), "Mouse Position = " + Input.mousePosition);
        GUI.Box(Rect((Screen.width / 2) - 70, Screen.height - 30, 140, 25), "Mouse X = " + Input.mousePosition.x);
        GUI.Box(Rect(5, (Screen.height / 2) - 12, 140, 25), "Mouse Y = " + Input.mousePosition.y);
    }*/
}
