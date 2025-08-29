using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectMultiplywithcopies", menuName = "Card/Effect/MultiplyWithCopies")]
public class SOCE_MultiplyWithCopies : SOC_Effect
{
    public StatType stat;
    public float multiplierPerCopy;
    public SOCTower copyChecked;

    public bool includeSelf;
    public override void ApplyTEffect(baseTowerScript script)
    {
        int copies = 0;
        if (!includeSelf)
        {
            copies -= 1;
        }
        foreach (baseTowerScript tower in gameControllerScript.instance.resourceManager.aliveTower)
        {
            if (tower.baseCard == copyChecked)
            {
                copies += 1;
            }
        }

        switch (stat)
        {
            case StatType.Damage:
                float endDamage = (float)script.damage * multiplierPerCopy * copies;
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce *= (int)multiplierPerCopy * copies;
                break;
            case StatType.SPA:
                script.secondsPerAttack *= multiplierPerCopy * copies;
                break;
            case StatType.Range:
                script.range *= multiplierPerCopy * copies;
                break;
            case StatType.Growth:
                script.multiplier *= multiplierPerCopy * copies;
                break;
            default:
                break;
        }
    }
    public override void ApplyPHEffect(sumObjT_Placeholder script)
    {
        int copies = 0;
        foreach (baseTowerScript tower in gameControllerScript.instance.resourceManager.aliveTower)
        {
            if (tower.baseCard == copyChecked)
            {
                copies += 1;
            }
        }

        switch (stat)
        {
            case StatType.Damage:
                float endDamage = (float)script.damage * multiplierPerCopy * copies;
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce *= (int)multiplierPerCopy * copies;
                break;
            case StatType.SPA:
                script.secondsPerAttack *= multiplierPerCopy * copies;
                break;
            case StatType.Range:
                script.range *= multiplierPerCopy * copies;
                break;
            case StatType.Growth:
                script.multiplier *= multiplierPerCopy * copies;
                break;
            default:
                break;
        }
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        int copies = 0;
        foreach (baseTowerScript tower in gameControllerScript.instance.resourceManager.aliveTower)
        {
            if (tower.baseCard == copyChecked)
            {
                copies += 1;
            }
        }

        switch (stat)
        {
            case StatType.Damage:
                float endDamage = (float)script.damage * multiplierPerCopy * copies;
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce *= (int)multiplierPerCopy * copies;
                break;
            default:
                break;
        }
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        int copies = 0;
        foreach (baseTowerScript tower in gameControllerScript.instance.resourceManager.aliveTower)
        {
            if (tower.baseCard == copyChecked)
            {
                copies += 1;
            }
        }

        switch (stat)
        {
            case StatType.Damage:
                float endDamage = (float)script.damage * multiplierPerCopy * copies;
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce *= (int)multiplierPerCopy * copies;
                break;
            default:
                break;
        }
    }
    public override void ApplyBEffect()
    {
        Debug.Log("TBA");
    }
}
