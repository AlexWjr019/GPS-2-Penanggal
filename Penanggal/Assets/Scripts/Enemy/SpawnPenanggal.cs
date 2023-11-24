using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPenanggal : MonoBehaviour
{
    private bool isSpawned;

    [SerializeField]
    private bool destroyObjSpawn = false;

    public float chaseDelay = 2f;

    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    [SerializeField]
    private Transform startPos, endPos;

    [SerializeField]
    private GameObject penanggal, cursePaper;

    private void OnEnable()
    {
        startTime = Time.time;
    }

    private void Start()
    {
        journeyLength = Vector3.Distance(startPos.position, endPos.position);
    }

    /*private void Update()
    {
        if (destroyObjSpawn)
        {
            if (cursePaper == null)
            {
                if (!isSpawned)
                {
                    Invoke("OnPenanggal", 3f);
                    isSpawned = true;
                }
            }
        }
        else
        {
            if (!isSpawned)
            {
                SpawnGhost();
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(cursePaper == null)
            {
                OnPenanggal();
                isSpawned = true;
            }
        }
    }

    public void SpawnGhost()
    {
        float distCovered = (Time.time - startTime) * speed;
    
        float fractionOfJourney = distCovered / journeyLength;
    
        penanggal.transform.position = Vector3.Lerp(startPos.position, endPos.position, fractionOfJourney);
    
        if (penanggal.transform.position == endPos.position)
        {
            Debug.Log("Penanggal is Spawned");
            FindObjectOfType<AudioManager>().PlaySFX("PenanggalLaugh");
            isSpawned = true;
            Invoke("OnAI_1", chaseDelay);
        }
    }

    private void OnAI_1()
    {
        penanggal.GetComponent<Penanggal>().enabled = true;
        Invoke("OnAI_2", 1f);
    }
    private void OnAI_2()
    {
        penanggal.GetComponent<NavMeshAgent>().enabled = true;
    }

    public void OnPenanggal()
    {
        penanggal.SetActive(true);
        Invoke("OnAI_1", 0.1f);
    }
}
