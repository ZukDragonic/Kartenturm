using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManagerScript : MonoBehaviour
{
    public bool isAutoStart;

    public int[] timeChanges;
    public int currentTime;

    public changeImageScript timeButton;

    public void changeTime()
    {
        if (Time.timeScale == 0 && currentTime != 0 && !isAutoStart)
        {
            timeButton.previousImage();
            resetTime();
            return;
        }
        currentTime++;
        if (currentTime > timeChanges.Length - 1)
        {
            currentTime -= timeChanges.Length;
        }

        Time.timeScale = timeChanges[currentTime];
    }

    public void resetTime()
    {
        Time.timeScale = timeChanges[currentTime];
    }
    public void pauseTime()
    {
        Time.timeScale = 0;
    }
    public void autoStart()
    {
        if (isAutoStart)
        {
            isAutoStart = false;
        }
        else
        {
            isAutoStart = true;
        }
    }
}
