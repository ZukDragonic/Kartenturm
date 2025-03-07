using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManagerScript : MonoBehaviour
{    
    public int[] timeChanges;
    public int currentTime;

    public changeImageScript timeButton;

    public void changeTime()
    {
        if (Time.timeScale == 0 && currentTime != 0 && !gameControllerScript.instance.FlagInfo(20001))
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
}
