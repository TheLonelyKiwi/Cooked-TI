using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitingState : BasePlayerState
{
    protected override bool canMove => true;
    
    private bool _isReady;

    protected override void OnActivate(){
        base.OnActivate();
        player.input.onInteractPressed += HandleInteractPressed;
        _isReady = false;
    }

    protected override void OnDeactivate(){
        base.OnDeactivate();
        player.input.onInteractPressed -= HandleInteractPressed;
    }

    private void HandleInteractPressed(){
        _isReady = !_isReady;
        EventBus.instance.onPlayerReadyChange?.Invoke(player, _isReady);
    }
}
