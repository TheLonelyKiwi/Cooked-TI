using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using JUtils;

public class PlayerJoinState : State
{
    private Dictionary<Player, bool> _playerReadyState = new();
    protected override void OnActivate(){
        SceneManager.LoadScene("PlayerScene");
        PlayerManager.instance.SetJoiningEnabled(true);

        foreach (Player player in PlayerManager.instance.players){
            HandlePlayerJoined(player);
        }

        EventBus.instance.onPlayerJoin += HandlePlayerJoined;
        EventBus.instance.onPlayerLeave += HandlePlayerLeft;
        EventBus.instance.onPlayerReadyChange += HandlePlayerReadyChanged;
    }

    protected override void OnDeactivate(){
        PlayerManager.instance.SetJoiningEnabled(false);
        EventBus.instance.onPlayerJoin -= HandlePlayerJoined;
        EventBus.instance.onPlayerLeave -= HandlePlayerLeft;
        EventBus.instance.onPlayerReadyChange -= HandlePlayerReadyChanged;
    }

    private void HandlePlayerJoined(Player player){
        _playerReadyState.Add(player, false);
    }

    private void HandlePlayerLeft(Player player){
        _playerReadyState.Remove(player);
    }

    private void HandlePlayerReadyChanged(Player player, bool isReady){
        _playerReadyState[player] = isReady;
        //if (_playerReadyState.Count <= 1) return;
        if (!_playerReadyState.All((it) => it.Value)) return;
        Debug.Log("Hello, we are super super ready");
    }
}

