using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectMultiplier", menuName = "Card/Effect/StatMultiplier")]
public class SOCE_Multiplier : SOC_Effect
{
    public StatType stat;
    public float multiplier;

    public int odds;
    public override void ApplyTEffect(baseTowerScript script)
    {
        if (Random.Range(0, odds) != 0)
        {
            return;
        }
        switch (stat)
        {
            case StatType.Damage:
                float endDamage = (float)script.damage * multiplier;
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce *= (int)multiplier;
                break;
            case StatType.SPA:
                script.secondsPerAttack *= multiplier;
                break;
            case StatType.Range:
                script.range *= multiplier;
                break;
            case StatType.Growth:
                script.multiplier *= multiplier;
                break;
            default:
                break;
        }
    }
    public override void ApplyPHEffect(sumObjT_Placeholder script)
    {
        if (Random.Range(0, odds) != 0)
        {
            return;
        }
        switch (stat)
        {
            case StatType.Damage:
                float endDamage = (float)script.damage * multiplier;
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce *= (int)multiplier;
                break;
            case StatType.SPA:
                script.secondsPerAttack *= multiplier;
                break;
            case StatType.Range:
                script.range *= multiplier;
                break;
            case StatType.Growth:
                script.multiplier *= multiplier;
                break;
            default:
                break;
        }
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        if (Random.Range(0, odds) != 0)
        {
            return;
        }
        switch (stat)
        {
            case StatType.Damage:
                float endDamage = (float)script.damage * multiplier;
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce *= (int)multiplier;
                break;
            default:
                break;
        }
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        if (Random.Range(0, odds) != 0)
        {
            return;
        }
        switch (stat)
        {
            case StatType.Damage:
                float endDamage = (float)script.damage * multiplier;
                script.damage = (int)endDamage;
                break;
            case StatType.Pierce:
                script.pierce *= (int)multiplier;
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
