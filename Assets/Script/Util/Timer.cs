using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float timer;
    private float defaultDuration;


    public Timer()
    {
        timer = 1f;
        defaultDuration = 1f;
    }

    public Timer(float duration)
    {
        timer = duration;
        defaultDuration = duration;
    }




    public float GetCurrentTime()
    {
        return timer;
    }

    public void SetTimer(float sec)
    {
        timer = sec;
    }
    public void SetTimer()
    {
        timer = defaultDuration;
    }

    public float GetDefaultDuration()
    {
        return defaultDuration;
    }

    public void CountDown()
    {
        timer -= 1 * Time.deltaTime;
    }
    
}


