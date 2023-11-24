using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemNotice : MonoBehaviour
{
    public TMP_Text lighterNoticeText;
    public TMP_Text burnNoticeText;
    public TMP_Text doorNoticeText;
    public TMP_Text pinboardNoticeText;
    public TMP_Text teachingNoticeText;
    public TMP_Text NoteText;
    public string lighterMessage = "Select the lighter and use on the candle";
    public string burnMessage = "I need to find something to burn this";
    public string doorMessage = "Perhaps I could find a key to unlock it";
    public string pinboardMessage = "I need to place something here";
    public string teachingMessage = "Tap to interact with highlighted objects";
    public string noteMessage = "Please put up the drawing once\n" +
                                "you are done with it";

    public float noticeDisplayTime = 3f;
    public static bool ligterPickup;

    // Start is called before the first frame update
    void Start()
    {
        //InventoryManager.Instance.OnItemSelected += ShowLighterNotice;
        ligterPickup = false;
        lighterNoticeText.gameObject.SetActive(false);
        burnNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        ShowTeachingNotice();
    }

    void Update()
    {
        if (ligterPickup)
        {
            ShowLighterNotice();
            ligterPickup = false;
        }
    }

    public void ShowLighterNotice()
    {
        lighterNoticeText.text = lighterMessage;
        lighterNoticeText.gameObject.SetActive(true);
        teachingNoticeText.gameObject.SetActive(false);
        burnNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);

        StartCoroutine(HideTeachingMessage());
    }

    public void ShowFlameNotice()
    {
        burnNoticeText.text = burnMessage;
        burnNoticeText.gameObject.SetActive(true);
        lighterNoticeText.gameObject.SetActive(false);
        teachingNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);

        StartCoroutine(HideMessage());
    }

    public void ShowFlameNotice2(string message)
    {
        burnNoticeText.text = message;
        burnNoticeText.gameObject.SetActive(true);
        lighterNoticeText.gameObject.SetActive(false);
        teachingNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);

        StartCoroutine(HideMessage());
    }

    public void ShowBurnedCursePaperNotice(int remainingCursePapers)
    {
        switch (remainingCursePapers)
        {
            case 3:
                ShowFlameNotice2("The number on the door change to 3");
                break;
            case 2:
                ShowFlameNotice2("The number on the door change to 2");
                break;
            case 1:
                ShowFlameNotice2("The number on the door change to 1");
                break;
            default:
                break;
        }
    }

    public void ShowDoorNotice()
    {
        doorNoticeText.text = doorMessage;
        doorNoticeText.gameObject.SetActive(true);
        burnNoticeText.gameObject.SetActive(false);
        lighterNoticeText.gameObject.SetActive(false);
        teachingNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);

        StartCoroutine(HideMessage());
    }

    public void ShowTeachingNotice()
    {
        teachingNoticeText.text = teachingMessage;
        teachingNoticeText.gameObject.SetActive(true);
        burnNoticeText.gameObject.SetActive(false);
        lighterNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);

        StartCoroutine(HideTeachingMessage());
    }

    public void ShowNoteText()
    {
        NoteText.text = noteMessage;
        NoteText.gameObject.SetActive(true);
        teachingNoticeText.gameObject.SetActive(false);
        burnNoticeText.gameObject.SetActive(false);
        lighterNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
    }

    public void ShowPinboardNotice()
    {
        pinboardNoticeText.text = pinboardMessage;
        pinboardNoticeText.gameObject.SetActive(true);
        doorNoticeText.gameObject.SetActive(false);
        burnNoticeText.gameObject.SetActive(false);
        lighterNoticeText.gameObject.SetActive(false);
        teachingNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        

        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(noticeDisplayTime);

        if (burnNoticeText.gameObject.activeSelf)
        {
            burnNoticeText.text = "";
            burnNoticeText.gameObject.SetActive(false);
        }
        if (doorNoticeText.gameObject.activeSelf)
        {
            doorNoticeText.text = "";
            doorNoticeText.gameObject.SetActive(false);
        }
        if (pinboardNoticeText.gameObject.activeSelf)
        {
            pinboardNoticeText.text = "";
            pinboardNoticeText.gameObject.SetActive(false);
        }
    }

    IEnumerator HideTeachingMessage()
    {
        yield return new WaitForSeconds(10);

        if (teachingNoticeText.gameObject.activeSelf)
        {
            teachingNoticeText.text = "";
            teachingNoticeText.gameObject.SetActive(false);
        }

        if (lighterNoticeText.gameObject.activeSelf)
        {
            lighterNoticeText.text = "";
            lighterNoticeText.gameObject.SetActive(false);
        }
    }
}