using JUtils;

public abstract class BasePlayerState : State
{
    protected virtual bool canMove => false;
    protected virtual bool canInteract => false;
    
    protected PlayerStateMachine playerStateMachine { get; private set; }
    protected PlayerMovement playerMovement => playerStateMachine.playerMovement;

    protected override void Awake()
    {
        base.Awake();
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
    }

    protected override void OnActivate()
    {
        if (canMove) {
            playerMovement.canMove = true;
        }
    }

    protected override void OnDeactivate()
    {
        if (canMove) {
            playerMovement.canMove = false;
        }
    }
}