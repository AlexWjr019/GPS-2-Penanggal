using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    int destPoint = 0;
    [HideInInspector]
    public NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GotoNextPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        if (points.Count == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Count;
    }
}