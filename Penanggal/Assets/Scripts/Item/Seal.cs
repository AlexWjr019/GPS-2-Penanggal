using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal : MonoBehaviour
{
    public GameObject[] cursePaper;
    public GameObject seal;
    public static bool cursePaperDestroyed;

    private int destroyedCount = 0;

    public void OnCursePaperDestroyed()
    {
        destroyedCount++;

        // Check if all "CursePaper" objects have been destroyed
        if (destroyedCount == 4)
        {
            cursePaperDestroyed = true;
            // Destroy the "seal" object
            Destroy(seal);
        }
    }
}
