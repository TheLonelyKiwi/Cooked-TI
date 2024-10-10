using JUtils;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player { get; private set; }

    protected override void Awake()
    {
        player = GetComponentInParent<Player>();
        base.Awake();
        Activate();
    }

    protected override void OnActivate()
    {
        ContinueQueue(); // Don't ask
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