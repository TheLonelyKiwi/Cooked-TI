using System.Linq;
using JUtils;
using UnityEngine;


public class Table : Interactable, IItemProvider, IItemDeposit
{
    [Space]
    [SerializeField, Required] private Inventory _inventory;

    public override bool CanInteract(Player player)
    {
        if (!CanGrabItem(player.inventory) && !CanDepositItem(player.inventory.LastItem())) return false;
        return base.CanInteract(player);
    }

    protected override void OnInteract(Player player)
    {
        if (CanGrabItem(player.inventory)) {
            player.stateMachine.GoToState<PlayerPickupState>(new StateData(this));
        } else if (CanDepositItem(player.inventory.LastItem())) {
            player.stateMachine.GoToState<PlayerPutdownState>(new StateData(this));
        }
    }

    public bool CanGrabItem(Inventory targetInventory)
    {
        return _inventory.HasItems() && targetInventory.IsNotFull();
    }

    public bool CanDepositItem(Item item)
    {
        return _inventory.IsNotFull();
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