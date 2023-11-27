using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEvent_8 : MonoBehaviour
{
    [SerializeField] public AudioSource chairTurning;
    [SerializeField] public BoxCollider boxCollider;

    public GameObject lastCursePaper;

    public GameObject[] deadBodiesLeft;
    public GameObject[] deadBodiesRight;

    public bool isActivated = false;

    public float leftRotation;
    public float rightRotation;

    private void Awake()
    {
        chairTurning = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && lastCursePaper == null)
        {
            isActivated = true;
            chairTurning.Play();
            boxCollider.enabled = false;
            Invoke("StopEvent", 1.6f);
        }
    }

    private void FixedUpdate()
    {
        if(isActivated && lastCursePaper == null)
        {
            for (int l = 0; l < deadBodiesLeft.Length; l++)
            {
                deadBodiesLeft[l].transform.Rotate(0, 0, leftRotation);
            }

            for (int r = 0; r < deadBodiesRight.Length; r++)
            {
                deadBodiesRight[r].transform.Rotate(0, 0, rightRotation);
            }
        }
    }   

    public void StopEvent()
    {
        leftRotation = 0;
        rightRotation = 0;
        isActivated = false;
    }
}
