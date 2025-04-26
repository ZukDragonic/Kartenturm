using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectBounce", menuName = "Card/Effect/Bounce")]
public class SOCE_Bounce : SOC_Effect
{
    public override void ApplyTEffect(baseTowerScript script)
    {
        Debug.Log("Does not exist");
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        script.gameObject.transform.rotation = Quaternion.Inverse(script.gameObject.transform.rotation);
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        script.gameObject.transform.rotation = Quaternion.Inverse(script.gameObject.transform.rotation);
    }
    public override void ApplyBEffect()
    {
        Debug.Log("TBA");
    }
}