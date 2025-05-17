using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectStun", menuName = "Card/Effect/Stun")]
public class SOCE_Stun : SOC_Effect
{
    public float duration;

    private int frameCount = 0;
    private float relativeDuration = 0;

    private float OGValue = 0;

    public override void ApplyEEffect(SOEnemyDisplay script)
    {
        if (OGValue == 0)
        {
            OGValue = script.speed;
            script.speed = 0;
        }

        relativeDuration = duration;

        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            script.speed = script.enemy.speed;
        }

        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyTEffect(baseTowerScript script)
    {
        if (OGValue == 0)
        {
            OGValue = script.CheckSummonStat(StatType.SPA);
            script.secondsPerAttack = 65536;
        }

        relativeDuration = duration;

        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            script.secondsPerAttack = OGValue;
        }

        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {

    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {

    }
}
