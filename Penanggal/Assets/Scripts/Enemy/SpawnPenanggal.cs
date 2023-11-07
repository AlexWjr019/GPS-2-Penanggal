using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPenanggal : MonoBehaviour
{
    private bool isSpawned;

    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    [SerializeField]
    private Transform startPos, endPos;

    [SerializeField]
    private GameObject penanggal;

    private void Awake()
    {
        startTime = Time.time;
    }

    private void Start()
    {
        journeyLength = Vector3.Distance(startPos.position, endPos.position);
    }

    private void Update()
    {
        SpawnGhost();
    }

    public void SpawnGhost()
    {
        if (!isSpawned)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / journeyLength;

            penanggal.transform.position = Vector3.Lerp(startPos.position, endPos.position, fractionOfJourney);

            if (penanggal.transform.position == endPos.position)
            {
                Debug.Log("Penanggal is Spawned");
                FindObjectOfType<AudioManager>().PlaySFX("PenanggalLaugh");
                isSpawned = true;
                Invoke("OnAI", 5f);
            }
        }
    }

    private void OnAI()
    {
        penanggal.GetComponent<Penanggal>().enabled = true;
        penanggal.GetComponent<NavMeshAgent>().enabled = true;
    }
}
