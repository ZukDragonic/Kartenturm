using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectRetarget", menuName = "Card/Effect/Retarget")]
public class SOCE_Retarget : SOC_Effect
{
    public ProjectileTargetOptions targetType;
    public int closestRange;

    public override void ApplyTEffect(baseTowerScript script)
    {
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        sumObj_Projectile pScript = script as sumObj_Projectile;
        switch (targetType)
        {
            case(ProjectileTargetOptions.Tower):
                if (pScript.parentTower.currentTarget != null)
                {
                    pScript.target = pScript.parentTower.currentTarget.transform.position;
                }
                break;
            case (ProjectileTargetOptions.Closest):
                if (gameControllerScript.instance.resourceManager.allEnemy.Count < 0)
                {
                    break;
                }
                float closestDistance = Vector3.Distance(gameControllerScript.instance.resourceManager.allEnemy[0].transform.position, script.transform.position);
                SOEnemyDisplay closestEnemy = gameControllerScript.instance.resourceManager.allEnemy[0];
                foreach (SOEnemyDisplay enemy in gameControllerScript.instance.resourceManager.allEnemy)
                {
                    float currentDistance = Vector3.Distance(enemy.transform.position, script.transform.position);
                    if (currentDistance < closestDistance)
                    {
                        closestDistance = currentDistance;
                        closestEnemy = enemy;
                    }
                }
                pScript.target = closestEnemy.transform.position;
                break;
            default:
                break;
        }
    }
    public override void ApplyBEffect()
    {
        Debug.Log("Does Not Exist");
    }
}
