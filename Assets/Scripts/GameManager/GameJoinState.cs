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
        _playerReadyState.Clear();
        SceneManager.LoadScene("PlayerScene");
        PlayerManager.instance.SetJoiningEnabled(true);
        
        PlayerJoinScreen.instance.Show();
        
        foreach (Player player in PlayerManager.instance.players){
            HandlePlayerJoined(player);
        }
        
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
            PlayerJoinScreen.instance.SetTimer(i);
            yield return new WaitForSeconds(1.5f);
        }
        
        stateMachine.GoToState<GamePlayState>();
    }

    private void HandlePlayerJoined(Player player){
        _playerReadyState.Add(player, false);
        player.stateMachine.GoToState<PlayerWaitingState>();
        player.transform.position = Random.insideUnitSphere.With(y: 0) * 3;
        PlayerJoinScreen.instance.AddPlayer(player, false);
        StopAllCoroutines();
        PlayerJoinScreen.instance.SetTimer(-1);
    }

    private void HandlePlayerLeft(Player player){
        PlayerJoinScreen.instance.RemovePlayer(player);
        _playerReadyState.Remove(player);
    }

    private void HandlePlayerReadyChanged(Player player, bool isReady){
        _playerReadyState[player] = isReady;
        if (!_playerReadyState.All(it => it.Value)) {
            PlayerJoinScreen.instance.SetTimer(-1);
            StopAllCoroutines();
            return;
        }

        StartCoroutine(StartGameRoutine());
    }
}

