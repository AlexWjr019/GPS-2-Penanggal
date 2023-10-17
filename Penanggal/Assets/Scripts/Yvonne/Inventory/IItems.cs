using System;
using UnityEngine;

public interface IItems
{
    string Name { get; }

    Sprite Image { get; }
    
    void OnPickup();

    void OnDrop();
    
    void OnUse();
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IItems item)
    {
        Item = item;
    }

    public IItems Item;
}
