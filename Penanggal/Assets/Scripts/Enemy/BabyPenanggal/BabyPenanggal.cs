using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyPenanggal : MonoBehaviour
{
    [SerializeField]
    public Vector3 playerPosition;

    [HideInInspector]
    public bool isSeen;

    private NavMeshAgent agent;
    [HideInInspector]
    public Vector3 spawnPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.SetDestination(playerPosition);
    }

    void Update()
    {
        if (!isSeen)
        {
            return;
        }
        else if (isSeen)
        {
            agent.SetDestination(spawnPoint);
            
            if (agent.remainingDistance < 0.2)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        transform.LookAt(playerPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }    
}
