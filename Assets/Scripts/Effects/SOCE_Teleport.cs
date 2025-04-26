using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectTeleport", menuName = "Card/Effect/Teleport")]
public class SOCE_Teleport : SOC_Effect
{
    public Vector3 localPos;
    public override void ApplyTEffect(baseTowerScript script)
    {
        Vector3 newPos = new Vector3(localPos.x + script.transform.position.x, localPos.y + script.transform.position.y, 0);
        script.transform.position = newPos;
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        script.transform.position = (script as sumObj_Projectile).target;
    }
}
