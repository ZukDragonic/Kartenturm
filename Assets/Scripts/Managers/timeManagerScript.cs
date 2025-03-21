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
        if (gameControllerScript.instance.state == GameState.BetweeWave)
        {
            gameControllerScript.instance.UpdateGameState(GameState.Wave);
            return;
        }
        currentTime++;
        timeButton.nextImage();
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

    public void buttonChange(int index)
    {
        if (index == -1)
        {
            timeButton.previousImage();
            return;
        }
        else if (index == -2)
        {
            timeButton.nextImage();
            return;
        }
        timeButton.selectImage(index);
    }
}
