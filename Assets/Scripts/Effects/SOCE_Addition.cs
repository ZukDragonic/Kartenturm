using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectAddition", menuName = "Card/Effect/StatAddition")]
public class SOCE_Addition : SOC_Effect
{
    public StatType stat;
    public int addition;

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
                script.attackValue += addition;
                break;
            case StatType.Pierce:
                script.projectilePierce += addition;
                break;
            case StatType.SPA:
                script.secondsPerAttack += addition;
                break;
            case StatType.Range:
                script.range += addition;
                break;
            case StatType.Growth:
                script.multiplier += addition;
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
        if (Random.Range(0, odds) != 0)
        {
            return;
        }
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
