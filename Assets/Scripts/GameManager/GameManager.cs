using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JUtils;

public class GameManager : StateMachine
{
    protected override void OnActivate()
    {
        DontDestroyOnLoad(gameObject);
        GoToState<PlayerJoinState>();
    }

    protected override void OnDeactivate()
    {
        ClearQueue();
        ContinueQueue();
    }

    protected override void OnNoState()
    {
        GoToState<PlayerJoinState>();
    }
}
