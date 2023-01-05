using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime;
    private int hours, minutes, seconds;

    public string GetCurrentTime()
    {
        return new string(hours + ":" + minutes + ":" + seconds);
    }

    private void Start()
    {
        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    private void UpdateTimer()
    {
        currentTime += 1;
        hours = (int)(currentTime / 3600);
        minutes = (int)((currentTime % 3600) / 60);
        seconds = (int)(currentTime % 60);
    }
}
