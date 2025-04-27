using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectGold", menuName = "Card/Effect/Gold")]
public class SOCE_Gold : SOC_Effect
{
    public int gold;
    public bool accountStat;
    public override void ApplyTEffect(baseTowerScript script)
    {
        int currentGold = gold;
        if (accountStat)
        {
            currentGold += (int)(script.damage * 2);
        }
        gameControllerScript.instance.resourceManager.changeGold(currentGold);
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        sumObj_Standalone sScript = script;
        int currentGold = gold;
        if (accountStat)
        {
            currentGold += (int)(sScript.damage * 2);
        }
        gameControllerScript.instance.resourceManager.changeGold(currentGold);
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        sumObj_Projectile pScript = script;
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
