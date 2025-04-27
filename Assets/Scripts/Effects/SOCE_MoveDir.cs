using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CardEffectMoveDir", menuName = "Card/Effect/MoveInDirection")]
public class SOCE_MoveDir : SOC_Effect
{
    public Vector3 dir;
    public float speed;
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        Debug.Log("TBA");
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        if (dir.y == 1)
        {
            script.transform.position += script.transform.right * speed * gameControllerScript.instance.timeManager.currentTime / 100;
        }
    }
    public override void ApplyBEffect()
    {
        Debug.Log("DNE");
    }
}
