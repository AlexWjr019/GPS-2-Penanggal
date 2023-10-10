using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.Shapes;

public class ItemNotice : MonoBehaviour
{
    public TMP_Text lighterNoticeText;
    public TMP_Text burnNoticeText;
    public string lighterMessage = "Maybe this can be useful for something";
    public string burnMessage = "I need something to burn this";
    public float noticeDisplayTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InventoryManager.Instance.OnItemSelected += ShowLighterNotice;
        lighterNoticeText.gameObject.SetActive(false);
        burnNoticeText.gameObject.SetActive(false);
    }

    public void ShowLighterNotice(string itemName)
    {
        lighterNoticeText.text = lighterMessage;
        lighterNoticeText.gameObject.SetActive(true);

        StartCoroutine(HideMessage());
    }

    public void ShowFlameNotice()
    {
        burnNoticeText.text = burnMessage;
        burnNoticeText.gameObject.SetActive(true);

        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(noticeDisplayTime);

        if (lighterNoticeText.gameObject.activeSelf)
        {
            lighterNoticeText.text = "";
            lighterNoticeText.gameObject.SetActive(false);
        }
        if (burnNoticeText.gameObject.activeSelf)
        {
            burnNoticeText.text = "";
            burnNoticeText.gameObject.SetActive(false);
        }
    }
}