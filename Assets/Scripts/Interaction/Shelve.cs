using System.Linq;
using JUtils;
using UnityEngine;


public class Shelve : Interactable, IItemProvider
{
    [Space]
    [SerializeField] private Transform _itemSpawnPosition;
    [SerializeField] private ItemData _itemData;

    public override bool CanInteract(Player player)
    {
        return base.CanInteract(player) && CanGrabItem(player.inventory);
    }

    protected override void OnInteract(Player player)
    {
        player.stateMachine.GoToState<PlayerPickupState>(new StateData(this));
    }

    public bool CanGrabItem(Inventory targetInventory)
    {
        return targetInventory.IsNotFull();
    }

    public Coroutine GrabItem(Inventory targetInventory)
    {
        Item item = Item.CreateItem(_itemData);
        item.transform.SetPositionAndRotation(_itemSpawnPosition.position, _itemSpawnPosition.rotation);
        targetInventory.TryAddItem(item, out Coroutine moveRoutine);
        return moveRoutine;
    }
}