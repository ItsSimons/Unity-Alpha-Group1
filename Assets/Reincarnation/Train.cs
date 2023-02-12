using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Train : MonoBehaviour
{
    public NavMeshAgent train;
    public Transform end;
    public float speed;

    private void Awake()
    {
        train.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Start()
    {
        train.destination = end.position;
        train.speed = speed;
    }
}
