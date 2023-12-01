using UnityEngine;
using UnityEngine.AI;

public class EndingAICutscene : MonoBehaviour
{
    public Transform targetPos;
    public Transform endPos;
    private NavMeshAgent ai;

    public bool goingBack = false;

    private void Awake()
    {
        ai = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(!goingBack)
        {
            ai.destination = targetPos.position;
        }
        else
        {
            ai.destination = endPos.position;
        }
    }
}
