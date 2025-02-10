using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManagerScript : MonoBehaviour
{
    public bool isAutoStart;

    public int[] timeChanges;
    public int currentTime;

    public void changeTime()
    {
        if (Time.timeScale == 0 && currentTime != 0 && !isAutoStart)
        {
            resetTime();
            return;
        }
        currentTime++;
        if (!isAutoStart)
        {
            if (currentTime > timeChanges.Length - 1)
            {
                currentTime--;
                isAutoStart = true;
            }
        }
        else
        {
            currentTime -= timeChanges.Length;
            isAutoStart = false;
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
