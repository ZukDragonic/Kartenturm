using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargettingFar", menuName = "Targetting/Far")]
public class targettingFar : targettingBase
{
    public override void EnterCheck(baseTowerScript script)
    {

    }
    public override void UpdateCheck(baseTowerScript script)
    {
        script.currentTarget = null;
        int furthestCheckpoint = -1;
        float furthestDistance = -1;
        float farest = -1;

        while (script.enemyInRange.Contains(null))
        {
            script.enemyInRange.Remove(null);
        }

        if (script.enemyInRange.Count > 0)
        {
            SOEnemyDisplay first = script.enemyInRange[0].GetComponent<SOEnemyDisplay>();

            furthestCheckpoint = first.checkpointNum;
            furthestDistance = first.distanceToCheckpoint;
            farest = Vector3.Distance(script.transform.position, first.gameObject.transform.position);
            script.currentTarget = first.gameObject;
        }

        foreach (GameObject enemy in script.enemyInRange)
        {
            SOEnemyDisplay enemyScript = enemy.GetComponent<SOEnemyDisplay>();

            float comparedDistance = Vector3.Distance(script.transform.position, enemy.gameObject.transform.position);
            //if enemy is farther, update
            if (comparedDistance > farest)
            {
                farest = comparedDistance;
                script.currentTarget = enemy;
            }
            else if (comparedDistance == farest)
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
        int FoundIndex = FindIndexInArray(script.allTargetting, this);

        //target change can only ever be -1, 0, or 1
        FoundIndex += script.targetChange;
        if (FoundIndex >= script.allTargetting.Length)
        {
            FoundIndex = 0;
        }
        else if (FoundIndex < 0)
        {
            FoundIndex = script.allTargetting.Length;
        }
        script.currentTargetting = script.allTargetting[FoundIndex];
        script.targetChange = 0;
    }
    public override string TargettingCheck()
    {
        return "Far";
    }
}