using JUtils;
using UnityEngine.SceneManagement;


public class GamePlayState : State
{
    protected override void OnActivate()
    {
        SceneManager.LoadScene("Game");
        EventManager.OnTimerStart();
        foreach (Player player in PlayerManager.instance.players) {
            player.stateMachine.GoToState<DefaultPlayerState>();
        }
    }

    protected override void OnDeactivate()
    {
    }
}