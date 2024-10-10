using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTimer : MonoBehaviour
{
    //does just that...
    void Start()
    {
        EventManager.OnTimerStart();
    }
}
