using System.Collections;
using System.Collections.Generic;
using JUtils;
using Unity.VisualScripting;
using UnityEngine;

public class Printer : Interactable, IItemProvider, IItemDeposit 
{
    [SerializeField, Required] private Inventory _inventory;
    [SerializeField, Required] private ItemData _printItemData;

    private float targetTime = 0f;
    
    bool isPrinting = false;
    public override bool CanInteract(Player player)
    {
        if(!CanGrabItem(player.inventory) && !CanDepositItem(player.inventory.LastItem()) && !isPrinting) return false;
        return base.CanInteract(player);
    }
    protected override void OnInteract(Player player)
    {
        if (CanGrabItem(player.inventory))
        {
            player.stateMachine.GoToState<PlayerPickupState>(new StateData(this));
        }
        else if (CanDepositItem(player.inventory.LastItem()))
        {
            player.stateMachine.GoToState<PlayerPutdownState>(new StateData(this));
        }
    }
    
    public bool CanGrabItem(Inventory targetInventory)
    {
        return _inventory.HasItems() && targetInventory.IsNotFull();
    }
    
    public bool CanDepositItem(Item item)
    {
        return _inventory.IsNotFull() && item != null;
    }
    
    public Coroutine GrabItem(Inventory targetInventory)
    {
        Item lastItem = _inventory.LastItem();
        _inventory.TryRemoveItem(lastItem); 
        targetInventory.TryAddItem(lastItem, out Coroutine moveRoutine);
        return (moveRoutine);
    }
    
    public Coroutine DepositItem(Item item)
    {
        _inventory.TryAddItem(item, out Coroutine moveRoutine);
        StartTimer(5.0f);
        return (moveRoutine);
    }

    private void StartTimer(float time)
    {
        targetTime = time;
        _inventory.TryRemoveItem(_inventory.LastItem());
        isPrinting = true;
    }

    private void EndTimer()
    {
        Item printItem = Item.CreateItem(_printItemData);
        printItem.transform.position = transform.position;
        _inventory.TryAddItem(printItem, out Coroutine moveRoutine);
        isPrinting = false;
    }

    void Update()
    {
        if (isPrinting)
        {
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                EndTimer();
            }
        }
    }

}
