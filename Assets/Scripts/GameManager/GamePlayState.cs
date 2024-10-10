using System.Collections.Generic;
using JUtils;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePlayState : State
{
    protected override void OnActivate()
    {
        SceneManager.LoadScene("Game");
        EventManager.OnTimerStop();
        foreach (Player player in PlayerManager.instance.players) {
            player.stateMachine.GoToState<DefaultPlayerState>();
        }

        EventBus.instance.onTimerFinished += HandleTimerStopped;
    }

    protected override void OnDeactivate()
    {
        EventManager.OnTimerStop();
        EventBus.instance.onTimerFinished -= HandleTimerStopped;
    }

    private void HandleTimerStopped()
    {
        stateMachine.GoToState<GameJoinState>();
    }
}