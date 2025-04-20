using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectDestroy", menuName = "Card/Effect/Destroy")]
public class SOCE_Destroy : SOC_Effect
{
    public float duration;
    //this needs to be a pPassive to work if duration != 0

    [HideInInspector]
    public int frameCount = 0;
    private float relativeDuration = 0;
    public override void ApplyTEffect(baseTowerScript script)
    {
        if (duration == 0)
        {
            Destroy(script.gameObject);
        }
        relativeDuration = duration;
        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            Destroy(script.gameObject);
        }
        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        if (duration == 0)
        {
            Destroy(script.gameObject);
        }
        relativeDuration = duration;
        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            Destroy(script.gameObject);
        }
        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        if (duration == 0)
        {
            Destroy(script.gameObject);
        }
        relativeDuration = duration * ((script as sumObj_Projectile).parentTower.range / (script as sumObj_Projectile).parentTower.baseCard.range * 0.1f + 1);
        //makes duration scale in respect to range

        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            Destroy(script.gameObject);
        }
        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyBEffect()
    {
        Debug.Log("TBA");
    }
}
