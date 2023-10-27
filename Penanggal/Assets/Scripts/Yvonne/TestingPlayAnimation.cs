using UnityEngine;

public class TestingPlayAnimation : MonoBehaviour
{
    public Animator cabinetAnimator;

    public bool isOpen = false;

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (gameObject.CompareTag("Interactable"))
        //    {
        //        if (!isOpen)
        //        {
        //            // play open animation
        //            isOpen = true;
        //            cabinetAnimator.SetBool("isOpen", true);
        //        }
        //        else
        //        {
        //            // play close animation
        //            isOpen = false;
        //            cabinetAnimator.SetBool("isOpen", false);
        //        }
        //    }
        //}

        if (!isOpen)
        {
            // play open animation
            //isOpen = true;
            cabinetAnimator.SetBool("isOpen", false);
        }
        else
        {
            // play close animation
            //isOpen = false;
            cabinetAnimator.SetBool("isOpen", true);
        }
    }
}
