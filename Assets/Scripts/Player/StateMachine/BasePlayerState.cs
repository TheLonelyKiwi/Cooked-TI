using System.Runtime.InteropServices;
using JUtils;
using UnityEngine;


public abstract class BasePlayerState : State
{
    protected virtual bool canMove => false;
    protected virtual bool canInteract => false;
    
    protected PlayerStateMachine playerStateMachine { get; private set; }
    protected Player player => playerStateMachine.player;

    protected override void Awake()
    {
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
        base.Awake();
    }

    protected override void OnActivate()
    {
        if (canMove) {
            player.input.onMovePressed += HandleMovePressed;
        }

        if (canInteract) {
            player.input.onInteractPressed += HandleInteractPressed;
        }
    }

    protected override void OnDeactivate()
    {
        if (canMove) {
            player.input.onMovePressed -= HandleMovePressed;
            player.movement.SetInput(Vector2.zero);
        }

        if (canInteract) {
            player.input.onInteractPressed -= HandleInteractPressed;
        }
    }

    private void HandleMovePressed(Vector2 input)
    {
        player.movement.SetInput(input);
    }

    private void HandleInteractPressed()
    {
        Interactable interactable = player.interactor.GetBestInteractable();
        if (interactable == null) return;
        stateMachine.GoToState<PlayerInteractState>(new StateData(interactable));
    }
}