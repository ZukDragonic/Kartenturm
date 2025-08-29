using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectAddwithcopies", menuName = "Card/Effect/AddWithCopies")]
public class SOCE_AddWithCopies : SOC_Effect
{
    public StatType stat;
    public float addPerCopy;
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
                float endDamage = (float)script.damage + (addPerCopy * copies);
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce += (int)addPerCopy * copies;
                break;
            case StatType.SPA:
                script.secondsPerAttack += addPerCopy * copies;
                break;
            case StatType.Range:
                script.range += addPerCopy * copies;
                break;
            case StatType.Growth:
                script.multiplier += addPerCopy * copies;
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
                float endDamage = (float)script.damage + (addPerCopy * copies);
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce += (int)addPerCopy * copies;
                break;
            case StatType.SPA:
                script.secondsPerAttack += addPerCopy * copies;
                break;
            case StatType.Range:
                script.range += addPerCopy * copies;
                break;
            case StatType.Growth:
                script.multiplier += addPerCopy * copies;
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
                float endDamage = (float)script.damage + (addPerCopy * copies);
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce += (int)addPerCopy * copies;
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
                float endDamage = (float)script.damage + (addPerCopy * copies);
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce += (int)addPerCopy * copies;
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
