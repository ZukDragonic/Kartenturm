using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectAddition", menuName = "Card/Effect/StatAddition")]
public class SOCE_Addition : SOC_Effect
{
    public StatType stat;
    public int addition;
    public override void ApplyTEffect(baseTowerScript script)
    {
        Debug.Log("TBA");
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        switch (stat)
        {
            case StatType.Damage:
                script.damage += addition;
                break;
            case StatType.Pierce:
                script.pierce += addition;
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
                script.damage += addition;
                break;
            case StatType.Pierce:
                script.pierce += addition;
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
