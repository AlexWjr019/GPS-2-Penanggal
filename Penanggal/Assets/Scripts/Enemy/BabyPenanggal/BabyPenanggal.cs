using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BabyPenanggal : MonoBehaviour
{
    //[SerializeField]
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

    public Camera playerCamera;
    public GameObject playerCam;
    public Camera aiCamera;
    public GameObject TomPuzzle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.autoBraking = false;
        agent.SetDestination(playerPosition);


        bPSpawn.Play();
        //FindObjectOfType<AudioManager>().PlaySFX("BabyPenSpeak1");

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Transform mainCameraTransform = player.transform.Find("Head/Main Camera");
            if (mainCameraTransform != null)
            {
                playerCamera = mainCameraTransform.GetComponent<Camera>();
            }
            else
            {
                Debug.LogError("Main Camera not found under Player/Head!");
            }
        }

        GameObject player1 = GameObject.Find("Player");
        if (player1 != null)
        {
            Transform mainCameraTransform = player1.transform.Find("Head/Main Camera");
            if (mainCameraTransform != null)
            {
                playerCam = mainCameraTransform.gameObject;
            }
        }

        GameObject cursedPaperPuzzles = GameObject.Find("CursedPaperPuzzles");
        if (cursedPaperPuzzles != null)
        {
            Transform tornDrawingTransform = cursedPaperPuzzles.transform.Find("TornDrawing");
            if (tornDrawingTransform != null)
            {
                Transform tornCameraTransform = tornDrawingTransform.Find("TornCamera");
                if (tornCameraTransform != null)
                {
                    TomPuzzle = tornCameraTransform.gameObject;
                }
                else
                {
                    Debug.LogError("TornCamera not found under TornDrawing!");
                }
            }
            else
            {
                Debug.LogError("TornDrawing not found under CursedPaperPuzzles!");
            }
        }
        else
        {
            Debug.LogError("CursedPaperPuzzles object not found in the scene!");
        }

        aiCamera = GetComponentInChildren<Camera>();

        if (aiCamera == null)
        {
            Debug.LogError("No Camera component found in the children of " + gameObject.name);
        }
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
            playerCam.SetActive(true);
            Debug.Log("Playing Attack Anim - Baby");
            bPSpawn.Play();
            playerCamera.enabled = false;
            aiCamera.enabled = true;
            if (TomPuzzle.activeSelf == true)
            {
                TomPuzzle.SetActive(false);
            }
            //FindObjectOfType<AudioManager>().PlaySFX("BabyPenSpeak1");
            animator.SetBool("isAttacking", true);
        }
    }    
}
