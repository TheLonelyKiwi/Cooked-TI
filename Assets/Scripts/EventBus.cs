using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JUtils;

public class EventBus : SingletonBehaviour<EventBus>
{
    public Action<Player> onPlayerJoin;
    public Action<Player> onPlayerLeave;
    public Action<Player, bool> onPlayerReadyChange;

   protected override void Awake(){
    DontDestroyOnLoad(gameObject);
    base.Awake();
   }
}
