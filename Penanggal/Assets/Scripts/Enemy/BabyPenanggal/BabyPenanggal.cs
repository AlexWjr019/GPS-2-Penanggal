using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BabyPenanggal : MonoBehaviour
{
    [SerializeField]
    public Vector3 playerPosition;
    [SerializeField]
    private float returnSpeed;

    [HideInInspector]
    public bool isSeen, isCalled;

    private NavMeshAgent agent;
    [HideInInspector]
    public Vector3 spawnPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.SetDestination(playerPosition);

        FindObjectOfType<AudioManager>().PlaySFX("BabyPenSpeak1");
    }

    void Update()
    {
        if (!isSeen)
        {
            return;
        }
        else if (isSeen)
        {
            Debug.Log("Baby is seen");
            agent.SetDestination(spawnPoint);
            agent.speed = returnSpeed;
            
            if (!isCalled)
            {
                isCalled = true;
                FindObjectOfType<AudioManager>().PlaySFX("BabyPenCrying3");
            }

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
            Destroy(gameObject);
        }
    }    
}
