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
            if (script.transform.GetChild(i).name != "Image")
            {
                script.transform.GetChild(i).localScale = new Vector3(scale * 2, scale * 2, scale * 2);
            }
            else
            {
                script.transform.GetChild(i).localScale = new Vector3(scale, scale, scale);
            }
        }
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        script.transform.localScale = new Vector3(scale, scale, scale);
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        script.transform.localScale = new Vector3(scale, scale, scale);
    }
    public override void ApplyBEffect()
    {
        Debug.Log("Does Not Exist");
    }
}
