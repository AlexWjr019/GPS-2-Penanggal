using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DebugText1 : MonoBehaviour
{
    public Text debugText;
    public NavMeshAgent navMeshAgent;

    void Start()
    {
       // navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (navMeshAgent != null)
        {
            debugText.text = "NavMeshAgent active: " + navMeshAgent.enabled;
        }
        else
        {
            debugText.text = "NavMeshAgent not found";
        }
    }

}
