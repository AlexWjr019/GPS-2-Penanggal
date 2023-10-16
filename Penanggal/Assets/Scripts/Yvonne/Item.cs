using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

public class Item : ScriptableObject
{
    public int itemId;
    public string itemName;
    public Sprite itemIcon;
}
