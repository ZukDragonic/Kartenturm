using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManagerScript : MonoBehaviour
{
    public int prevTime;
    public int currentTime;
    public int maxTime;

    public changeImageScript timeButton;

    public int currentFps;
    public float secondsPerFrame;

    public void StartAction()
    {
        if (GameObject.Find("Time") != null)
        {
            timeButton = GameObject.Find("Time").GetComponent<changeImageScript>();
        }
    }

    public void changeTime()
    {
        if (gameControllerScript.instance.state == GameState.BetweenWave)
        {
            gameControllerScript.instance.UpdateGameState(GameState.Wave);
            return;
        }
        currentTime++;
        prevTime = currentTime;
        timeButton.nextImage();
        if (currentTime > maxTime)
        {
            currentTime -= maxTime;
        }
    }

    public void resetTime()
    {
        currentTime = prevTime;
    }
    public void pauseTime()
    {
        prevTime = currentTime;
        currentTime = 0;
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

    public void ChangeFps(int Fps)
    {
        Application.targetFrameRate = Fps;
        currentFps = Fps;
        secondsPerFrame = 1 / Fps;
    }
}
