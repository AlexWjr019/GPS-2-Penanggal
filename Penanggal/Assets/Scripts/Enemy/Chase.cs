using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField]
    float lookDistance = 5;
    [SerializeField]
    LayerMask layerMask;
    //[SerializeField]
    //GameObject player;

    Patrol p;

    Vector3 fwd;

    private void Start()
    {
        p = GetComponent<Patrol>();
    }

    void Update()
    {
        fwd = transform.TransformDirection(Vector3.forward) * lookDistance;
        Debug.DrawRay(transform.position, fwd, Color.green);
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, fwd, out RaycastHit hit, lookDistance, layerMask))
        {
            Debug.Log(hit.collider.gameObject.name + " was hit");
            p.agent.ResetPath();
            p.agent.SetDestination(hit.transform.position);
        }
    }
}
