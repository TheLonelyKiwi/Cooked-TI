using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPutdownState : BasePlayerState
{
    protected override void OnActivate()
    {
        base.OnActivate();
        StartCoroutine(PickupItem(stateData.Get<Interactable>(0)));
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
    }

    private IEnumerator PickupItem(Interactable interactable)
    {
    }
}