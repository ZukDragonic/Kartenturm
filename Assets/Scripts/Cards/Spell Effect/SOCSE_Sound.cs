using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCSE_Sound : SOCSEffect
{
    public int soundID;
    public override void ApplyEffect()
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
}
