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

    private Animator animator;

    public AudioSource bPSpawn;
    public AudioSource bPDespawn;

    //public Camera playerCamera;
    //public Camera aiCamera;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.autoBraking = false;
        agent.SetDestination(playerPosition);


        bPSpawn.Play();
        //FindObjectOfType<AudioManager>().PlaySFX("BabyPenSpeak1");

        //GameObject player = GameObject.Find("Player");
        //if (player != null)
        //{
        //    Transform mainCameraTransform = player.transform.Find("Head/Main Camera");
        //    if (mainCameraTransform != null)
        //    {
        //        playerCamera = mainCameraTransform.GetComponent<Camera>();
        //    }
        //    else
        //    {
        //        Debug.LogError("Main Camera not found under Player/Head!");
        //    }
        //}

        //GameObject babyPenanggalObj = GameObject.Find("BabyPenanggal");
        //if (babyPenanggalObj != null)
        //{
        //    aiCamera = babyPenanggalObj.GetComponentInChildren<Camera>();
        //}
        //else
        //{
        //    Debug.LogError("BabyPenanggal object not found in the scene!");
        //}
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
                bPSpawn.Stop();
                bPDespawn.Play();
                //FindObjectOfType<AudioManager>().PlaySFX("BabyPenCrying3");
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
            Debug.Log("Playing Attack Anim - Baby");
            //aiCamera.enabled = true;
            //playerCamera.enabled = false;
            bPSpawn.Play();
            //FindObjectOfType<AudioManager>().PlaySFX("BabyPenSpeak1");
            animator.SetBool("isAttacking", true);
        }
    }    
}
