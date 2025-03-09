using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectDestroy", menuName = "Card/Effect/Destroy")]
public class SOCE_Destroy : SOC_Effect
{
    public float duration;
    public override void ApplyTEffect(baseTowerScript script)
    {
        Destroy(script.gameObject, duration);
    }
    public override void ApplyPEffect(sumObj_Base script)
    {
        //makes duration scale in respect to range
        duration *= ((script as sumObj_Projectile).parentTower.range / (script as sumObj_Projectile).parentTower.baseCard.range);
        Destroy(script.gameObject, duration);
    }
    public override void ApplyBEffect()
    {
        Debug.Log("TBA");
    }
}
