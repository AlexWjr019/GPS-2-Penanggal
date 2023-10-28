using UnityEngine;

public class TestingPlayAnimation : MonoBehaviour
{
    public Animator cabinetAnimator;

    public GameObject[] cabinets;

    public bool[] isOpen;

    void Update()
    {
        if (cabinets[0] && !isOpen[0])
        {
            cabinetAnimator.SetBool("isOpen", false);
        }
        else
        {
            cabinetAnimator.SetBool("isOpen", true);
        }
    }
}
