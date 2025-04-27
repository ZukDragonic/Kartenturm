using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectRotate", menuName = "Card/Effect/Rotate")]
public class SOCE_Rotate : SOC_Effect
{
    public int rot;
    public override void ApplyTEffect(baseTowerScript script)
    {
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        Vector3 tempRot = script.transform.rotation.eulerAngles;
        tempRot.z += rot;
        script.transform.rotation = Quaternion.Euler(tempRot);
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        Vector3 tempRot = script.transform.rotation.eulerAngles;
        tempRot.z += rot;
        script.transform.rotation = Quaternion.Euler(tempRot);
    }
    public override void ApplyBEffect()
    {
        Debug.Log("TBA");
    }
}