using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Train : MonoBehaviour
{
    private const int V = 0;
    public NavMeshAgent train;
    public Transform end;
    private object updatePosition;

    private void Awake()
    {
        train.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Start()
    {
        bool shoudc = false;
        do
        {
            train.destination = end.position;
        } while(shoudc == true);
    }
}
