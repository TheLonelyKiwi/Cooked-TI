
using UnityEngine;


public interface IInteractable
{
    public bool isLocked { get; set; }
    public void Interact(Player player);
    public bool CanInteract(Player player);
}

public interface IItemDeposit : IInteractable
{
    public bool CanDepositItem(Item item);
    public Coroutine DepositItem(Item item);
}

public interface IItemProvider : IInteractable
{
    public bool CanGrabItem(Inventory targetInventory);
    public Coroutine GrabItem(Inventory targetInventory);
}
