using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildbButtonPressed : MonoBehaviour
{
    public GameObject[] prefabs = null;
    public Camera cam = null;

    // whats being built
    public GameObject target;

    private void Update()
    {
        InstantiateObject();
    }

    public void GreenBuilding()
    {
        target = prefabs[0];
    }

    public void YellowBuilding()
    {
        target = prefabs[1];
    }

    public void InstantiateObject()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (target != null)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        Instantiate(target, hit.point, Quaternion.identity);
                        target = null;
                    }
                }
                
            }
        }
    }
}
