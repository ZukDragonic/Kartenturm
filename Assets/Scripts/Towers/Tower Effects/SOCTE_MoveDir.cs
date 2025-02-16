using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CardTowerEffectMoveDir", menuName = "Card/TowerEffect/MoveInDirection")]
public class SOCTE_MoveDir : SOCTEffect
{
    private Vector3 dir;
    public float speed;
    public override void ApplyPEffect(projectileScript script)
    {
        dir = (script.target - script.parentTower.transform.position).normalized;
        script.transform.position += dir * speed * Time.deltaTime;
        Debug.Log(dir);
    }
}
