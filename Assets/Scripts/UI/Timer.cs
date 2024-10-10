using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    enum TimerType {Countdown, Stopwatch}
    [SerializeField] private TimerType timerType;
    [SerializeField] private float timeToDisplay = 60.0f;
    [SerializeField] private TMP_Text _timerText;
    
    private string _originalText;

    private bool _isRunning;
    
    private void OnEnable(){
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdate;
    }

    private void OnDisable(){
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
    }

    private void EventManagerOnTimerStart() => _isRunning = true;
    private void EventManagerOnTimerStop() => _isRunning = false;
    private void EventManagerOnTimerUpdate(float value) => timeToDisplay += value;

    private void Start()
    {
        _originalText = _timerText.text;
    }

    private void Update()
    {
        if (!_isRunning) return;
        if (timerType == TimerType.Countdown && timeToDisplay < 0.0f) {
            EventBus.instance.onTimerFinished?.Invoke();
        }

        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);

        string timeText;

        if (timeToDisplay <= 30) {
            timeText = timeSpan.ToString(format:@"mm\:ss\:ff");
            _timerText.color = Color.HSVToRGB(0, .8f, 1f);
        } else {
            timeText = timeSpan.ToString(format:@"mm\:ss");
            _timerText.color = Color.white;
        }
        
        _timerText.text = $"{_originalText} {timeText}";
    }
}
