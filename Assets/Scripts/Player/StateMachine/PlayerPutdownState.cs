using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerPutdownState : BasePlayerState
{
    private IItemDeposit _interactable;
    
    protected override void OnActivate()
    {
        base.OnActivate();
        _interactable = stateData.Get<IItemDeposit>(0);
        _interactable.isLocked = true;
        StartCoroutine(PickupItem());
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
        _interactable.isLocked = false;
    }

    private IEnumerator PickupItem()
    {
        Item item = player.inventory.items.Last();
        player.inventory.TryRemoveItem(item);
        yield return _interactable.DepositItem(item);
        stateMachine.ContinueQueue();
    }
}