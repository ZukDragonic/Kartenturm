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
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        //spaghetti fix that impacts PERFORMANCE or LAG
        int enemyListCount = gameControllerScript.instance.resourceManager.aliveEnemy.Count + 1;
        for (int i = 0; i < enemyListCount; i++)
        {
            gameControllerScript.instance.resourceManager.aliveEnemy.Remove(null);
        }

        if (gameControllerScript.instance.resourceManager.aliveEnemy.Count <= 0)
        {
            return;
        }
        sumObj_Projectile pScript = script;
        switch (targetType)
        {
            case(ProjectileTargetOptions.Tower):
                if (pScript.parentTower.currentTarget != null)
                {
                    pScript.target = pScript.parentTower.currentTarget.transform.position;
                }
                break;
            case (ProjectileTargetOptions.Closest):
                if (gameControllerScript.instance.resourceManager.aliveEnemy.Count < 0)
                {
                    break;
                }
                float closestDistance = Vector3.Distance(gameControllerScript.instance.resourceManager.aliveEnemy[0].transform.position, script.transform.position);
                SOEnemyDisplay closestEnemy = gameControllerScript.instance.resourceManager.aliveEnemy[0];
                foreach (SOEnemyDisplay enemy in gameControllerScript.instance.resourceManager.aliveEnemy)
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
