using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectMultiplier", menuName = "Card/Effect/StatMultiplier")]
public class SOCE_Multiplier : SOC_Effect
{
    public StatType stat;
    public float multiplier;
    public override void ApplyTEffect(baseTowerScript script)
    {
        switch (stat)
        {
            case StatType.Damage:
                script.attackValue *= multiplier;
                break;
            case StatType.Pierce:
                script.projectilePierce *= (int)multiplier;
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
    public override void ApplyOSEffect(sumObj_Base script)
    {
        switch (stat)
        {
            case StatType.Damage:
                script.damage *= multiplier;
                break;
            case StatType.Pierce:
                script.pierce *= (int)multiplier;
                break;
            default:
                break;
        }
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        switch (stat)
        {
            case StatType.Damage:
                script.damage *= multiplier;
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
