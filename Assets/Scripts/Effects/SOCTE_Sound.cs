using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCTE_Sound : SOC_Effect
{
    public int soundID;
    public override void ApplyTEffect(baseTowerScript script)
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
    public override void ApplyPEffect(sumObj_Base script)
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
    public override void ApplyBEffect()
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
}
