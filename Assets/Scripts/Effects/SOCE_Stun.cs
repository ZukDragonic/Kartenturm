using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectStun", menuName = "Card/Effect/Stun")]
public class SOCE_Stun : SOC_Effect
{
    public float duration;

    public int frameCount = 0;
    public float relativeDuration = 0;

    public float OGEnemySpeed = 0;

    public override void ApplyEEffect(SOEnemyDisplay script)
    {
        if (OGEnemySpeed == 0)
        {
            OGEnemySpeed = script.speed;
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

    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {

    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {

    }
}
