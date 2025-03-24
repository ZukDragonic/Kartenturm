using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCE_Gold : SOC_Effect
{
    public int gold;
    public bool accountStat;
    public override void ApplyTEffect(baseTowerScript script)
    {
        int currentGold = gold;
        if (accountStat)
        {
            currentGold += (int)(script.attackValue * 2);
        }
        gameControllerScript.instance.resourceManager.changeGold(currentGold);
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        sumObj_Standalone sScript = script as sumObj_Standalone;
        int currentGold = gold;
        if (accountStat)
        {
            currentGold += (int)(sScript.damage * 2);
        }
        gameControllerScript.instance.resourceManager.changeGold(currentGold);
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        sumObj_Projectile pScript = script as sumObj_Projectile;
        int currentGold = gold;
        if (accountStat)
        {
            currentGold += (int)(pScript.damage * 2);
        }
        gameControllerScript.instance.resourceManager.changeGold(currentGold);
    }
    public override void ApplyBEffect()
    {
        gameControllerScript.instance.resourceManager.changeGold(gold);
    }
}
