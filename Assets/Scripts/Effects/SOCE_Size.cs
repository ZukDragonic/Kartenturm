using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectSize", menuName = "Card/Effect/Size")]
public class SOCE_Size : SOC_Effect
{
    public float scale;

    public override void ApplyTEffect(baseTowerScript script)
    {
        for (int i = 1; i < script.gameObject.transform.childCount; i++)
        {
            float ogScale = script.transform.GetChild(i).localScale.x;
            script.transform.GetChild(i).localScale = new Vector3(scale * ogScale, scale * ogScale, scale * ogScale);
        }
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        float ogScale = script.transform.localScale.x;
        script.transform.localScale = new Vector3(scale * ogScale, scale * ogScale, scale * ogScale);
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        float ogScale = script.transform.localScale.x;
        script.transform.localScale = new Vector3(scale * ogScale, scale * ogScale, scale * ogScale);
    }
    public override void ApplyBEffect()
    {
        Debug.Log("Does Not Exist");
    }
}
