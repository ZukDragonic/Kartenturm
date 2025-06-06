using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectLifetime", menuName = "Card/Effect/Life Time")]
public class SOCE_LifeTime : SOC_Effect
{
    public float duration;
    //this needs to be a pPassive to work if duration != 0

    [HideInInspector]
    private int frameCount = 0;
    private float relativeDuration = 0;

    public float radius;
    public override void ApplyEEffect(SOEnemyDisplay script)
    {
        if (duration == 0)
        {
            gameControllerScript.instance.resourceManager.removeEnemy(script);
        }
        relativeDuration = duration;
        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            gameControllerScript.instance.resourceManager.removeEnemy(script);
        }
        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyTEffect(baseTowerScript script)
    {
        if (duration == 0)
        {
            gameControllerScript.instance.resourceManager.removeTower(script);
        }
        relativeDuration = duration;
        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            gameControllerScript.instance.resourceManager.removeTower(script);
        }
        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
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
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        if (duration == 0)
        {
            Destroy(script.gameObject);
        }
        relativeDuration = duration * (script.parentTower.range / script.parentTower.baseCard.range * 0.1f + 1);
        //makes duration scale in respect to range

        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            Destroy(script.gameObject);
        }
        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyBEffect()
    {
        waveManagerScript wm = gameControllerScript.instance.waveManager;
        resourceManagerScript rm = gameControllerScript.instance.resourceManager;
        Vector3 mousePos = Input.mousePosition;

        SOEnemyDisplay[] aliveEnemy = rm.aliveEnemy.ToArray();
        baseTowerScript[] aliveTower = rm.aliveTower.ToArray();

        foreach (SOEnemyDisplay enemy in aliveEnemy)
        {
            float distance = Vector3.Distance(enemy.transform.position, mousePos);
            if (distance >= radius)
            {
                rm.changeGold(enemy.enemy.goldDrop);
                rm.removeEnemy(enemy);
            }
        }
        foreach (baseTowerScript tower in aliveTower)
        {
            float distance = Vector3.Distance(tower.transform.position, mousePos);
            if (distance >= radius)
            {
                rm.removeTower(tower);
            }
        }
    }
}
