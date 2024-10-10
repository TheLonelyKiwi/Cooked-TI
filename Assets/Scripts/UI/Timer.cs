using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text _timerText;
    enum TimerType {Countdown}
    [SerializeField] private TimerType timerType;

    [SerializeField] private float timeToDisplay = 60.0f;

    private bool _isRunning;

    private void Awake(){
        _timerText = GetComponent<TMP_Text>();
    }
  

    private void OnEnable(){
        EventBus.instance.timerStart += EventManagerOnTimerStart;
        EventBus.instance.timerStop += EventManagerOnTimerStop;
        EventBus.instance.timerUpdate += EventManagerOnTimerUpdate;
    }

    private void OnDisable(){
        EventBus.instance.timerStart -= EventManagerOnTimerStart;
        EventBus.instance.timerStop -= EventManagerOnTimerStop;
        EventBus.instance.timerUpdate -= EventManagerOnTimerUpdate;
    }

    private void EventManagerOnTimerStart() => _isRunning = true;

    private void EventManagerOnTimerStop() => _isRunning = false;
    private void EventManagerOnTimerUpdate(float value) => timeToDisplay += value;


    private void Update()
    {
        if (!_isRunning) return;
        if(timerType == TimerType.Countdown && timeToDisplay < 0.0f) return;



        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;



        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        _timerText.text = timeSpan.ToString(format:@"mm\:ss\:ff");
    }
}
