using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JUtils;

public class GameManager : StateMachine
{
    protected override void Start(){
        base.Start();
        EventManager.OnTimerStart();
    }
    protected override void OnActivate()
    {
        DontDestroyOnLoad(gameObject);
        GoToState<GameJoinState>();
    }

    protected override void OnDeactivate()
    {
        ClearQueue();
        ContinueQueue();
    }

    protected override void OnNoState()
    {
        GoToState<GameJoinState>();
    }
}
