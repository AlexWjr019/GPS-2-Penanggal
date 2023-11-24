using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LockControl : MonoBehaviour
{
    private int[] result, correctCombination;
    public GameObject wheel;
    public Animation safeOpen;
    private Quaternion originalRotation; // Store the original rotation
    public static bool safeIsOpen;
    public GameObject cursepaper;
    private int itemsMask;

    private void Awake()
    {
        itemsMask = LayerMask.NameToLayer("Items");
    }

    private void Start()
    {
        result = new int[] { 0, 0, 0, 0 };
        correctCombination = new int[] { 0, 9, 1, 6 };
        Safecode.Rotated += CheckResults;

        // Store the original rotation when the script starts
        originalRotation = transform.localRotation;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "Lock1":
                result[0] = number;
                break;

            case "Lock2":
                result[1] = number;
                break;

            case "Lock3":
                result[2] = number;
                break;
            case "Lock4":
                result[3] = number;
                break;
        }
        Debug.Log("Current Combination: " + string.Join(", ", result));
        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2] && result[3] == correctCombination[3])
        {
            Debug.Log("Opened!");
            cursepaper.SetActive(true);
            cursepaper.layer = itemsMask;
            FindObjectOfType<AudioManager>().PlaySFX("LockUnlock");
            safeOpen.Play("OpenSafe");
            wheel.gameObject.SetActive(false);
            safeIsOpen = true;
            OpenSafeCode.openSafe = false;
            ObjectiveManager.objective = true;
        }
        else
        {
            FindObjectOfType<AudioManager>().PlaySFX("WrongCombinationLock");
        }
    }

    // Add a method to reset the wheel's rotation
    public void ResetRotation()
    {
        transform.localRotation = originalRotation;
        OpenSafeCode.openSafe = false;
    }

    private void OnDestroy()
    {
        Safecode.Rotated -= CheckResults;
    }
}