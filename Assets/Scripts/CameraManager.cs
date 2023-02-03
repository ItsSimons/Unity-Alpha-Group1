using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime;

    [SerializeField] private int boundary = 20;

    private int screenWidth;
    private int screenHeight;

    [SerializeField] private Vector3 newPosition;

    [SerializeField] private int zoomMax;
    [SerializeField] private int zoomMin;

    void Start()
    {
        newPosition = transform.position;

        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    private void LateUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || (Input.mousePosition.y > screenHeight - boundary))
        {
            var move = new Vector3(moveSpeed, 0, moveSpeed);
            newPosition += (move * Camera.main.orthographicSize / zoomMax);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || (Input.mousePosition.y < 0 + boundary))
        {
            var move = new Vector3(-moveSpeed, 0, -moveSpeed);
            newPosition += (move * Camera.main.orthographicSize / zoomMax);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || (Input.mousePosition.x > screenWidth - boundary))
        {
            newPosition += (transform.right * moveSpeed * Camera.main.orthographicSize / zoomMax);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || (Input.mousePosition.x < 0 + boundary))
        {
            newPosition += (transform.right * -moveSpeed * Camera.main.orthographicSize / zoomMax);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + 1, zoomMin, zoomMax);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - 1, zoomMin, zoomMax);
        }

        transform.position = newPosition;
    }

    /*void OnGUI()
    {
        GUI.Box(Rect((Screen.width / 2) - 140, 5, 280, 25), "Mouse Position = " + Input.mousePosition);
        GUI.Box(Rect((Screen.width / 2) - 70, Screen.height - 30, 140, 25), "Mouse X = " + Input.mousePosition.x);
        GUI.Box(Rect(5, (Screen.height / 2) - 12, 140, 25), "Mouse Y = " + Input.mousePosition.y);
    }*/
}
