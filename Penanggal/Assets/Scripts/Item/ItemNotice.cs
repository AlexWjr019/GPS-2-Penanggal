using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemNotice : MonoBehaviour
{
    public TMP_Text lighterNoticeText;
    public TMP_Text burnNoticeText;
    public TMP_Text lightUpNoticeText;
    public TMP_Text doorNoticeText;
    public TMP_Text nurseryDoorNoticeText;
    public TMP_Text pinboardNoticeText;
    public TMP_Text teachingNoticeText;
    public TMP_Text NoteText;
    public TMP_Text NoteText2;
    public string lighterMessage = "Select the lighter and use on the candle";
    public string burnCursepaperMessage = "I need to find something to burn this";
    public string lightUpMessage = "I need to find something to light up this";
    public string doorMessage = "Perhaps I could find the bedroom key";
    public string nurseryDoorMessage = "Perhaps I could find the nursery room key";
    public string pinboardMessage = "I need to place something here";
    public string teachingMessage = "Tap to interact with highlighted objects";
    public string noteMessage = "Please put up the drawing once\n" +
                                "you are done with it";
    public string noteMessage2 = "I need to find a way to bind her to me. The cursed paper should do the job. This will protect me from her.\n" + "\nOh my dear ain, you will never leave me ever again. Your eternal beauty shall last forever and so will you";

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
        NoteText2.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        lightUpNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);
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
        NoteText2.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        lightUpNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);

        StartCoroutine(HideTeachingMessage());
    }

    public void ShowLightUpNotice()
    {
        lightUpNoticeText.text = lightUpMessage;
        lightUpNoticeText.gameObject.SetActive(true);
        burnNoticeText.gameObject.SetActive(false);
        lighterNoticeText.gameObject.SetActive(false);
        teachingNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        NoteText2.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);

        StartCoroutine(HideMessage());
    }

    public void ShowNurseryDoorNotice()
    {
        nurseryDoorNoticeText.text = nurseryDoorMessage;
        nurseryDoorNoticeText.gameObject.SetActive(true);
        lightUpNoticeText.gameObject.SetActive(false);
        burnNoticeText.gameObject.SetActive(false);
        lighterNoticeText.gameObject.SetActive(false);
        teachingNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        NoteText2.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);

        StartCoroutine(HideMessage());
    }

    public void ShowFlameNotice()
    {
        burnNoticeText.text = burnCursepaperMessage;
        burnNoticeText.gameObject.SetActive(true);
        lighterNoticeText.gameObject.SetActive(false);
        teachingNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        NoteText2.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        lightUpNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);

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
        NoteText2.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        lightUpNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);

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
        NoteText2.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        lightUpNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);

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
        NoteText2.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        lightUpNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);

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
        NoteText2.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        lightUpNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);
    }

    public void ShowNoteText2()
    {
        NoteText2.text = noteMessage2;
        NoteText2.gameObject.SetActive(true);
        NoteText.gameObject.SetActive(false);
        teachingNoticeText.gameObject.SetActive(false);
        burnNoticeText.gameObject.SetActive(false);
        lighterNoticeText.gameObject.SetActive(false);
        doorNoticeText.gameObject.SetActive(false);
        pinboardNoticeText.gameObject.SetActive(false);
        lightUpNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);
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
        NoteText2.gameObject.SetActive(false);
        lightUpNoticeText.gameObject.SetActive(false);
        nurseryDoorNoticeText.gameObject.SetActive(false);


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
        if (lightUpNoticeText.gameObject.activeSelf)
        {
            lightUpNoticeText.text = "";
            lightUpNoticeText.gameObject.SetActive(false);
        }
        if (nurseryDoorNoticeText.gameObject.activeSelf)
        {
            nurseryDoorNoticeText.text = "";
            nurseryDoorNoticeText.gameObject.SetActive(false);
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