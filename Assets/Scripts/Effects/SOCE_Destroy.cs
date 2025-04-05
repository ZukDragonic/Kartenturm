using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectDestroy", menuName = "Card/Effect/Destroy")]
public class SOCE_Destroy : SOC_Effect
{
    public float duration;
    public int frameCount = 0;
    private float relativeDuration = 0;
    public override void ApplyTEffect(baseTowerScript script)
    {
        relativeDuration = duration;
        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            Destroy(script.gameObject);
        }
        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        relativeDuration = duration;
        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            Destroy(script.gameObject);
        }
        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        //this needs to be a pPassive to work
        //makes duration scale in respect to range
        relativeDuration = duration * ((script as sumObj_Projectile).parentTower.range / (script as sumObj_Projectile).parentTower.baseCard.range * 0.1f + 1);
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
