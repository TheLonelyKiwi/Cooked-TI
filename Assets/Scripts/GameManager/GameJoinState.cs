using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using JUtils;

public class GameJoinState : State
{
    private Dictionary<Player, bool> _playerReadyState = new();
    
    protected override void OnActivate() {
        SceneManager.LoadScene("PlayerScene");
        PlayerManager.instance.SetJoiningEnabled(true);

        foreach (Player player in PlayerManager.instance.players){
            HandlePlayerJoined(player);
        }
        
        PlayerJoinScreen.instance.Show();
        
        EventBus.instance.onPlayerJoin += HandlePlayerJoined;
        EventBus.instance.onPlayerLeave += HandlePlayerLeft;
        EventBus.instance.onPlayerReadyChange += HandlePlayerReadyChanged;
    }

    protected override void OnDeactivate(){
        PlayerManager.instance.SetJoiningEnabled(false);
        PlayerJoinScreen.instance.Hide();
        EventBus.instance.onPlayerJoin -= HandlePlayerJoined;
        EventBus.instance.onPlayerLeave -= HandlePlayerLeft;
        EventBus.instance.onPlayerReadyChange -= HandlePlayerReadyChanged;
    }

    private IEnumerator StartGameRoutine()
    {
        for (int i = 3; i > 0; i--) {
            Debug.Log($"Starting in {i} seconds");
            yield return new WaitForSeconds(1);
        }
        
        stateMachine.GoToState<GamePlayState>();
    }

    private void HandlePlayerJoined(Player player){
        _playerReadyState.Add(player, false);
        player.stateMachine.GoToState<PlayerWaitingState>();
        PlayerJoinScreen.instance.AddPlayer(player, false);
        StopAllCoroutines();
    }

    private void HandlePlayerLeft(Player player){
        PlayerJoinScreen.instance.RemovePlayer(player);
        _playerReadyState.Remove(player);
    }

    private void HandlePlayerReadyChanged(Player player, bool isReady){
        _playerReadyState[player] = isReady;
        if (!_playerReadyState.All(it => it.Value)) {
            StopAllCoroutines();
            return;
        }

        StartCoroutine(StartGameRoutine());
    }
}

