using System.Linq;
using JUtils;
using UnityEngine;


public class CraftingStation : Interactable, IItemDeposit, IItemProvider
{
    [Space]
    [SerializeField, Required] private MachineType _machineType;
    [SerializeField, Required] private Inventory _inventory;

    public override bool CanInteract(Player player)
    {
        return base.CanInteract(player) && CanDepositItem(player.inventory.LastItem());
    }

    protected override void OnInteract(Player player)
    {
    }

    public bool CanGrabItem(Inventory targetInventory)
    {
        return _inventory.HasItems() && targetInventory.IsNotFull();
    }

    public bool CanDepositItem(Item item)
    {
        if (item == null) return false;
        return _machineType.IsValidRecipeWithItem(_inventory.items.Select(it => it.itemData), item.itemData);
    }

    public Coroutine GrabItem(Inventory targetInventory)
    {
        Item lastItem = _inventory.LastItem();
        _inventory.TryRemoveItem(lastItem);
        targetInventory.TryAddItem(lastItem, out Coroutine moveRoutine);
        return moveRoutine;
    }

    public Coroutine DepositItem(Item item)
    {
        _inventory.TryAddItem(item, out Coroutine moveRoutine);
        return moveRoutine;
    }
}