using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using JUtils;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    public List<Player> players { get; private set;} = new List<Player>();

    private PlayerInputManager _inputManager;

    public void SetJoiningEnabled(bool enabled){
       if (enabled) {
        _inputManager.EnableJoining();
       }
       else{
        _inputManager.DisableJoining();
       }
    }

    protected override void Awake(){
        _inputManager = GetComponent<PlayerInputManager>();
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }

    private void OnPlayerJoined(UnityEngine.InputSystem.PlayerInput playerInput){
        Player player = playerInput.GetComponentInParent<Player>();
            DontDestroyOnLoad(player.gameObject);
            player.transform.parent = transform;
        players.Add(player);
        EventBus.instance.onPlayerJoin?.Invoke(player);
    }

    private void OnPlayerLeft(UnityEngine.InputSystem.PlayerInput playerInput){
        Player player = playerInput.GetComponentInParent<Player>();
        EventBus.instance.onPlayerLeave?.Invoke(player);
        players.Remove(player);
    }
}

