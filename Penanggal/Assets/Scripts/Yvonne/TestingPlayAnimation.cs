using UnityEngine;

public class TestingPlayAnimation : MonoBehaviour
{
    public Animator cabinetAnimator;
    public GameObject[] cabinets;
    public BoxCollider[] closedColliders;
    public BoxCollider[] openedColliders;
    public bool[] isOpen;

    void Update()
    {
        if (cabinets[0] && !isOpen[0])
        {
            cabinetAnimator.SetBool("isOpen", false);
            closedColliders[0].enabled = true;
            openedColliders[0].enabled = false;
            openedColliders[1].enabled = false;
        }
        else
        {
            cabinetAnimator.SetBool("isOpen", true);
            closedColliders[0].enabled = false;
            openedColliders[0].enabled = true;
            openedColliders[1].enabled = true;
        }
    }
}
