using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targettingStrong : targettingBase
{
    public override void EnterCheck(baseTowerScript script)
    {

    }
    public override void UpdateCheck(baseTowerScript script)
    {
        script.currentTarget = null;
        float highestHp = -1;
        int furthestCheckpoint = -1;
        float furthestDistance = -1;

        if (script.enemyInRange.Count > 0)
        {
            SOEnemyDisplay first = script.enemyInRange[0].GetComponent<SOEnemyDisplay>();

            furthestCheckpoint = first.checkpointNum;
            furthestDistance = first.distanceToCheckpoint;
            highestHp = first.health;
            script.currentTarget = first.gameObject;
        }

        foreach (GameObject enemy in script.enemyInRange)
        {
            SOEnemyDisplay enemyScript = enemy.GetComponent<SOEnemyDisplay>();

            float comparedHp = enemyScript.health;
            //if enemy is higher hp, update
            if (comparedHp > highestHp)
            {
                highestHp = comparedHp;
                script.currentTarget = enemy;
            }
            else if (comparedHp == highestHp)
            {
                int comparedCpNum = enemyScript.checkpointNum;
                //if enemy is on further checkpoint, update
                if (comparedCpNum > furthestCheckpoint)
                {
                    furthestCheckpoint = comparedCpNum;
                    furthestDistance = enemyScript.distanceToCheckpoint;
                    script.currentTarget = enemy;
                }
                else if (comparedCpNum == furthestCheckpoint)
                {
                    float currentDistance = enemyScript.distanceToCheckpoint;
                    //if enemy is further from checkpoint, update
                    if (currentDistance < furthestDistance)
                    {
                        furthestDistance = currentDistance;
                        script.currentTarget = enemy;
                    }
                }
            }
        }
    }
    public override void ExitCheck(baseTowerScript script)
    {
        if (script.targetChange == -1)
        {
            script.currentTargetting = script.targetLast;
        }
        else
        {
            script.currentTargetting = script.targetWeak;
        }

        script.targetChange = 0;
    }
    public override string TargettingCheck()
    {
        return "Strong";
    }
}