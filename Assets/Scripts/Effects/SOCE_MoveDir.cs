using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CardEffectMoveDir", menuName = "Card/Effect/MoveInDirection")]
public class SOCE_MoveDir : SOC_Effect
{
    private Vector3 dir;
    public float speed;
    public override void ApplyOSEffect(sumObj_Base script)
    {
        Debug.Log("TBA");
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        dir = ((script as sumObj_Projectile).target - (script as sumObj_Projectile).parentTower.transform.position).normalized;
        script.transform.position += dir * speed * Time.deltaTime;
    }
}
