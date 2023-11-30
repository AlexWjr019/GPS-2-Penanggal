using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public static bool noteSeen;
    public static bool noteIsSeen;
    public GameObject pauseButton;
    public bool note, note2;

    private void Start()
    {
        noteSeen = false;
        noteIsSeen = false;
    }

    private void Update()
    {
        if (noteSeen)
        {
            if (note)
            {
                ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
                if (itemNotice != null)
                {
                    itemNotice.ShowNoteText();
                    noteSeen = false;
                    noteIsSeen = true;
                    pauseButton.SetActive(false);
                }
            }
            if (note2)
            {
                ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
                if (itemNotice != null)
                {
                    itemNotice.ShowNoteText2();
                    noteSeen = false;
                    noteIsSeen = true;
                    pauseButton.SetActive(false);
                }
            }

        }
    }

    public void Return()
    {
        noteIsSeen = false;
        pauseButton.SetActive(true);
    }
}
