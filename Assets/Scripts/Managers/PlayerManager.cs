using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using JUtils;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    public List<Player> players { get; private set;} = new List<Player>();

    protected override void Awake(){
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }

    private void OnPlayerJoined(UnityEngine.InputSystem.PlayerInput playerInput){
        Player player = playerInput.GetComponentInParent<Player>();
        StartCoroutine(Routines.DelayRoutine(1f, () => {
            DontDestroyOnLoad(player.gameObject);
            player.transform.parent = transform;
        }));
        players.Add(player);
    }

    private void OnPlayerLeft(UnityEngine.InputSystem.PlayerInput playerInput){
        Player player = playerInput.GetComponentInParent<Player>();
        players.Remove(player);
    }

    [Button]
    private void SwitchToGameScene(){
        SceneManager.LoadScene("Game");
    }
}

