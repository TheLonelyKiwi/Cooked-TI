using System;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField] private int _maxItemCapacity;
    [SerializeField] private Transform[] _itemVisuals;

    public List<Item> items { get; private set; } = new();
    public Item[] _visualOccupiedMap;
    
    public event Action<Item> onItemAdded;
    public event Action<Item> onItemRemoved;

    private void Awake()
    {
        if (_maxItemCapacity > _itemVisuals.Length) {
            Debug.LogError($"Inventory for {gameObject} has more capacity than visual spots", this);
        }

        _visualOccupiedMap = new Item[_maxItemCapacity];
        for (int i = 0; i < _visualOccupiedMap.Length; i++) {
            _visualOccupiedMap[i] = true;
        }
    }

    public bool TryAddItem(Item item)
    {
        if (items.Count >= _maxItemCapacity + 1) return false;
        if (item.inventory == this) return false;
        if (item.inventory != null) {
            if (!item.inventory.TryRemoveItem(item)) return false;
        }

        Transform itemSpot = null;
        for (int i = 0; i < _maxItemCapacity; i++) {
            if (!_visualOccupiedMap[i]) continue;
            _visualOccupiedMap[i] = item;
            itemSpot = _itemVisuals[i % _itemVisuals.Length];
            break;
        }
        
        item.inventory = this;
        item.MoveTowards(itemSpot);
        items.Add(item);
        return true;
    }

    public bool TryRemoveItem(Item item)
    {
        if (item.inventory != this) return false;
        item.inventory = null;
        
        for (int i = 0; i < _visualOccupiedMap.Length; i++) {
            if (_visualOccupiedMap[i] != item) continue;
            _visualOccupiedMap[i] = null;
            break;
        }
        
        return items.Remove(item);
    }
}