using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCTE_Sound : SOCTEffect
{
    public int soundID;
    public override void ApplyTEffect(baseTowerScript script)
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
    public override void ApplyPEffect(projectileScript script)
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
}
