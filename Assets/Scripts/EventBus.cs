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

    public Action timerStart;
    public Action timerStop;
    public Action<float> timerUpdate;

   protected override void Awake(){
    DontDestroyOnLoad(gameObject);
    base.Awake();
   }
}
