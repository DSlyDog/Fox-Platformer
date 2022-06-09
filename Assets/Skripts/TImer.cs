using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TImer
{
    float currentTime;
    float timerStart;
    float timerEnd;

    public TImer(float duration)
    {
        timerStart = Time.deltaTime;
        currentTime = Time.deltaTime;
        timerEnd = timerStart + duration;
    }

    public float getCurrentTime()
    {
        currentTime += Time.deltaTime;
        return timerEnd - currentTime;
    }

    public bool isTimerOver()
    {
        return currentTime >= timerEnd;
    }
}
