using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using JUtils;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    private (Color color, String colorName)[] colorMap;
    
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
        
        colorMap = new []{
           (color: Color.HSVToRGB(0, .6f, 1f), colorName: "Red"),
           (color: Color.HSVToRGB(0.28f, .6f, 1f), colorName: "Green"),
           (color: Color.HSVToRGB(0.5f, .6f, 1f), colorName: "Blue"),
           (color: Color.HSVToRGB(0.75f, .6f, 1f), colorName: "Purple"),
       };
    }

    private void OnPlayerJoined(UnityEngine.InputSystem.PlayerInput playerInput){
        Player player = playerInput.GetComponentInParent<Player>();
        DontDestroyOnLoad(player.gameObject);

        var color = colorMap.First(colorName => players.All(p => p.colorName != colorName.colorName));
        player.transform.parent = transform;
        player.colorName = color.colorName;
        player.color = color.color;
        
        players.Add(player);
        EventBus.instance.onPlayerJoin?.Invoke(player);
    }

    private void OnPlayerLeft(UnityEngine.InputSystem.PlayerInput playerInput){
        Player player = playerInput.GetComponentInParent<Player>();
        EventBus.instance.onPlayerLeave?.Invoke(player);
        players.Remove(player);
    }
}

