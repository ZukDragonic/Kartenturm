using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectOverride", menuName = "Card/Effect/StatOverride")]
public class SOCE_Override : SOC_Effect
{
    public StatType stat;
    public int newStat;
    public override void ApplyTEffect(baseTowerScript script)
    {
        Debug.Log("TBA");
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        switch (stat)
        {
            case StatType.Damage:
                script.damage = newStat;
                break;
            case StatType.Pierce:
                script.pierce = newStat;
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
                script.damage = newStat;
                break;
            case StatType.Pierce:
                script.pierce = newStat;
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
