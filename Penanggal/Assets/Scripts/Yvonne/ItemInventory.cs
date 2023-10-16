using System;
using UnityEngine;

public interface ItemInventory
{
    string Name { get; }

    Sprite Image { get; }

    void OnPickup();
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(ItemInventory item)
    {
        Item = item;
    }

    public ItemInventory Item;
}
