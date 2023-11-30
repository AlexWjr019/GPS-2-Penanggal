using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note5 : MonoBehaviour
{
    public static bool noteSeen;
    public static bool noteIsSeen;
    public GameObject pauseButton;

    private void Start()
    {
        noteSeen = false;
        noteIsSeen = false;
    }

    private void Update()
    {
        if (noteSeen)
        {
            ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
            if (itemNotice != null)
            {
                itemNotice.ShowNoteText5();
                noteSeen = false;
                noteIsSeen = true;
                pauseButton.SetActive(false);
            }
        }
    }

    public void Return()
    {
        noteIsSeen = false;
        pauseButton.SetActive(true);
    }
}
