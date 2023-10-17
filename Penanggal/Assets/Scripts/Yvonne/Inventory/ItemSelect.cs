using UnityEngine;

public class ItemSelect : MonoBehaviour
{
    public Inventory _Inventory;

    public void OnItemSelected()
    {
        ItemDrag dragHandler =
        gameObject.transform.Find("ItemImage").GetComponent<ItemDrag>();

        IItems item = dragHandler.Item;

        Debug.Log(item.Name);

        _Inventory.UseItem(item);

        item.OnUse();
    }
}
