using System;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField] private int _maxItemCapacity;
    [SerializeField] private Transform[] _itemVisuals;

    public List<Item> items { get; private set; } = new();
    
    public event Action<Item> onItemAdded;
    public event Action<Item> onItemRemoved;

    public bool TryAddItem(Item item)
    {
        if (items.Count >= _maxItemCapacity + 1) return false;
        if (item.inventory == this) return false;
        if (item.inventory != null) {
            if (!item.inventory.TryRemoveItem(item)) return false;
        }
        item.inventory = this;
        items.Add(item);
        return true;
    }

    public bool TryRemoveItem(Item item)
    {
        if (item.inventory != this) return false;
        item.inventory = null;
        return items.Remove(item);
    }
}