using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectLookattarget", menuName = "Card/Effect/LookAtTarget")]
public class SOCE_LookAtTarget : SOC_Effect
{
    public int maxRot;
    public override void ApplyTEffect(baseTowerScript script)
    {
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {

    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        Vector3 target = (script as sumObj_Projectile).target;

        //face target
        target.z = script.transform.position.z;
        target = script.transform.position - target;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.left, target);
        // Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, target.z));
        float referenceZ = script.gameObject.transform.rotation.eulerAngles.z;
        float eRotZ = rotation.eulerAngles.z;
        int rotMultiplier = gameControllerScript.instance.timeManager.currentTime;
        eRotZ = Mathf.Clamp(rotation.eulerAngles.z,
            referenceZ - maxRot * rotMultiplier,
            referenceZ + maxRot * rotMultiplier);
        //if object attmpts max rotation in wrong direction, force other direction
        //a small rotation in other direction is fine to make straight targetting
        if (eRotZ == referenceZ - (maxRot * rotMultiplier))
        {
            eRotZ = eRotZ + (maxRot * 2 * rotMultiplier);
        }
        rotation.eulerAngles = new Vector3(0,0, eRotZ);
        script.gameObject.transform.rotation = rotation;
    }
    public override void ApplyBEffect()
    {
        Debug.Log("TBA");
    }
}