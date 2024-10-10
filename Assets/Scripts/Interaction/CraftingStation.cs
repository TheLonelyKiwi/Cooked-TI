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
        if (!base.CanInteract(player)) return false;
        if (CanDepositItem(player.inventory.LastItem())) return true;
        if (player.inventory.IsEmpty() && _machineType.TryGetRecipesForItem(_inventory.items.Select(it => it.itemData), out _)) return true;
        if (CanGrabItem(player.inventory)) return true;
        return true;
    }

    protected override void OnInteract(Player player)
    {
        Recipe recipe;
        if (CanDepositItem(player.inventory.LastItem())) {
            if (_machineType.TryGetRecipesForItem(_inventory.items.Append(player.inventory.LastItem()).Select(it => it.itemData), out recipe)) {
                player.stateMachine.AddToQueue<PlayerCraftingState>(new StateData(this, recipe), queueFirst: true);
            }
            player.stateMachine.AddToQueue<PlayerPutdownState>(new StateData(this), queueFirst: true);
            player.stateMachine.ContinueQueue();
            return;
        }

        if (player.inventory.IsEmpty() && _machineType.TryGetRecipesForItem(_inventory.items.Select(it => it.itemData), out recipe)) {
            player.stateMachine.GoToState<PlayerCraftingState>(new StateData(this, recipe));
            return;
        }

        if (CanGrabItem(player.inventory)) {
            player.stateMachine.GoToState<PlayerPickupState>(new StateData(this));
            return;
        }
    }

    public void FinishedCrafting()
    {
        if (!_machineType.TryGetRecipesForItem(_inventory.items.Select(it => it.itemData), out Recipe recipe)) return;
        while (_inventory.HasItems()) {
            Item itemToDie = _inventory.LastItem();
            _inventory.TryRemoveItem(itemToDie);
            Destroy(itemToDie.gameObject);
        }

        Item item = Item.CreateItem(recipe.output);
        item.transform.position = transform.position;
        _inventory.TryAddItem(item, out Coroutine routine);
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