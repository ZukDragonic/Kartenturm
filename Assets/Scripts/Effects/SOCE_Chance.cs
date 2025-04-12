using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectChance", menuName = "Card/Effect/Chance")]
public class SOCE_Chance : SOC_Effect
{
    public SOC_Effect executedEffect;
    public int odds;

    public override void ApplyTEffect(baseTowerScript script)
    {
        if (Random.Range(0, odds) == 0)
        {
            executedEffect.ApplyTEffect(script);
        }
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        if (Random.Range(0, odds) == 0)
        {
            executedEffect.ApplyOSEffect(script);
        }
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        if (Random.Range(0, odds) == 0)
        {
            executedEffect.ApplyOPEffect(script);
        }
    }
    public override void ApplyBEffect()
    {
        if (Random.Range(0, odds) == 0)
        {
            executedEffect.ApplyBEffect();
        }
    }
}
