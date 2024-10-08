using JUtils;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerMovement playerMovement { get; private set; }

    protected override void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        base.Awake();
    }

    protected override void OnActivate()
    {
        GoToState<DefaultPlayerState>();
    }

    protected override void OnDeactivate()
    {
        ClearQueue();
        ContinueQueue();
    }

    protected override void OnNoState()
    {
        GoToState<DefaultPlayerState>();
    }
}