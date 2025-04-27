using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectOverride", menuName = "Card/Effect/StatOverride")]
public class SOCE_Override : SOC_Effect
{
    public StatType stat;
    public float newStat;

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
                script.damage = newStat;
                break;
            case StatType.Pierce:
                script.pierce = (int)newStat;
                break;
            case StatType.SPA:
                script.secondsPerAttack = newStat;
                break;
            case StatType.Range:
                script.range = newStat;
                break;
            case StatType.Growth:
                script.multiplier = newStat;
                break;
            default:
                break;
        }
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        if (Random.Range(0, odds) != 0)
        {
            return;
        }
        switch (stat)
        {
            case StatType.Damage:
                script.damage = newStat;
                break;
            case StatType.Pierce:
                script.pierce = (int)newStat;
                break;
            default:
                break;
        }
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        if (Random.Range(0, odds) != 0)
        {
            return;
        }
        switch (stat)
        {
            case StatType.Damage:
                script.damage = newStat;
                break;
            case StatType.Pierce:
                script.pierce = (int)newStat;
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
