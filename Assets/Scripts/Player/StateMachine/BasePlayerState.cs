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
    }

    protected override void OnDeactivate()
    {
        if (canMove) {
            player.input.onMovePressed -= HandleMovePressed;
            player.movement.SetInput(Vector2.zero);
        }
    }

    private void HandleMovePressed(Vector2 input)
    {
        player.movement.SetInput(input);
    }
}