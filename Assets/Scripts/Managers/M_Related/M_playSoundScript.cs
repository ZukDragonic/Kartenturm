using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_playSoundScript : MonoBehaviour
{
    public int soundID;
    void PlaySound()
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
}
