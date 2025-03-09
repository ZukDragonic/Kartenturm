using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectMultiplier", menuName = "Card/Effect/StatMultiplier")]
public class SOCE_Multiplier : SOC_Effect
{
    public StatType stat;
    public int multiplier;
    public override void ApplyTEffect(baseTowerScript script)
    {
        Debug.Log("TBA");
    }
    public override void ApplyPEffect(sumObj_Base script)
    {
        switch (stat)
        {
            case StatType.Damage:
                script.damage *= multiplier;
                break;
            case StatType.Pierce:
                script.pierce *= multiplier;
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
