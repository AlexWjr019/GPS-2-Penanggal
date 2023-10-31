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

    private void Awake()
    {
        
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        }
    }

    private void FixedUpdate()
    {
        transform.rotation.SetLookRotation(playerPosition);
    }

    public void OnDestroy()
    {
        BabySpawner.Instance.babies.Remove(gameObject);
    }
}
