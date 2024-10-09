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
    protected override void Awake(){
        DontDestroyOnLoad(gameObject);
        base.Awake();
        _inputManager = GetComponent<PlayerInputManager>();
        _inputManager.onPlayerJoined += HandlePlayerJoined;
        _inputManager.onPlayerLeft += HandlePlayerLeft;
    }

    protected override void OnDestroy(){
        base.OnDestroy();
        _inputManager.onPlayerJoined -= HandlePlayerJoined;
        _inputManager.onPlayerLeft -= HandlePlayerLeft;
    }

    private void HandlePlayerJoined(UnityEngine.InputSystem.PlayerInput playerInput){
        Player player = playerInput.GetComponentInParent<Player>();
        StartCoroutine(Routines.DelayRoutine(1f, () => {
            DontDestroyOnLoad(player.gameObject);
            player.transform.parent = transform;
        }));
        players.Add(player);
    }

    private void HandlePlayerLeft(UnityEngine.InputSystem.PlayerInput playerInput){
        Player player = playerInput.GetComponentInParent<Player>();
        players.Remove(player);
    }

    [Button]
    private void SwitchToGameScene(){
        SceneManager.LoadScene("Game");
    }
}

