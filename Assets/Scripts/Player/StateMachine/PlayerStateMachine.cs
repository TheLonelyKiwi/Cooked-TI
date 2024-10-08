using JUtils;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player { get; private set; }

    protected override void Awake()
    {
        player = GetComponentInParent<Player>();
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