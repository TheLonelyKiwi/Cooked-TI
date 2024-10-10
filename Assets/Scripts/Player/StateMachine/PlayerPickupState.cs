using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPickupState : BasePlayerState
{
    protected override void OnActivate()
    {
        base.OnActivate();
        StartCoroutine(PickupItem(stateData.Get<Item>(0)));
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
    }

    private IEnumerator PickupItem(Item item)
    {
        if (!player.inventory.TryAddItem(item, out Coroutine moveRoutine)) yield break;
        yield return moveRoutine;
        stateMachine.ContinueQueue();
    }
}