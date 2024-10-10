using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerPickupState : BasePlayerState
{
    private IItemProvider _interactable;
    
    protected override void OnActivate()
    {
        base.OnActivate();
        _interactable = stateData.Get<IItemProvider>(0);
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
        yield return _interactable.GrabItem(player.inventory);
        stateMachine.ContinueQueue();
    }
}