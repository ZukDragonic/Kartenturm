using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectRetarget", menuName = "Card/Effect/Retarget")]
public class SOCE_Retarget : SOC_Effect
{
    public override void ApplyTEffect(baseTowerScript script)
    {
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        sumObj_Projectile pScript = script as sumObj_Projectile;
        if (pScript.parentTower.currentTarget != null)
        {
            pScript.target = pScript.parentTower.currentTarget.transform.position;
        }
    }
    public override void ApplyBEffect()
    {
        Debug.Log("Does Not Exist");
    }
}
