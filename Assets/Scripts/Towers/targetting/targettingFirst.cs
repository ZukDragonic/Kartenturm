using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargettingFirst", menuName = "Targetting/First")]
public class targettingFirst : targettingBase
{
    public override void EnterCheck(baseTowerScript script)
    {

    }
    public override void UpdateCheck(baseTowerScript script)
    {
        script.currentTarget = null;
        int furthestCheckpoint = -1;
        float furthestDistance = -1;

        int enemyListCount = script.enemyInRange.Count + 1;
        for (int i = 0; i < enemyListCount; i++)
        {
            script.enemyInRange.Remove(null);
        }

        if (script.enemyInRange.Count > 0)
        {
            SOEnemyDisplay first = script.enemyInRange[0].GetComponent<SOEnemyDisplay>();

            furthestCheckpoint = first.checkpointNum;
            furthestDistance = first.distanceToCheckpoint;
            script.currentTarget = first.gameObject;
        }

        foreach (GameObject enemy in script.enemyInRange)
        {
            SOEnemyDisplay enemyScript = enemy.GetComponent<SOEnemyDisplay>();

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
    public override void ExitCheck(baseTowerScript script)
    {
        int FoundIndex = FindIndexInArray(script.allTargetting, this);

        //target change can only ever be -1, 0, or 1
        FoundIndex += script.targetChange;
        if (FoundIndex >= script.allTargetting.Length)
        {
            FoundIndex = 0;
        }
        else if (FoundIndex < 0)
        {
            FoundIndex = script.allTargetting.Length - 1;
        }
        script.currentTargetting = script.allTargetting[FoundIndex];
        script.targetChange = 0;
    }
    public override string TargettingCheck()
    {
        return "First";
    }
}
