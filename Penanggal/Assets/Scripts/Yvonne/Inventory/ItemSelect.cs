using UnityEngine;

public class ItemSelect : MonoBehaviour
{
    public void OnItemSelected()
    {
        ItemSelect itemSelect =
        gameObject.transform.Find("ItemImage").GetComponent<ItemSelect>();

        //IItems item = itemSelect.Item;

        //Debug.Log(item.Name);

        //item.OnUse();
    }
}
