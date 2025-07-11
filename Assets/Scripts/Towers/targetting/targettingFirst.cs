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
        float furthestDistance = -1;

        while (script.enemyInRange.Contains(null))
        {
            script.enemyInRange.Remove(null);
        }

        if (script.enemyInRange.Count > 0)
        {
            SOEnemyDisplay first = script.enemyInRange[0].GetComponent<SOEnemyDisplay>();

            furthestDistance = first.totalRemainingDistance;
            script.currentTarget = first.gameObject;
        }

        foreach (GameObject enemy in script.enemyInRange)
        {
            SOEnemyDisplay enemyScript = enemy.GetComponent<SOEnemyDisplay>();

            float currentDistance = enemyScript.totalRemainingDistance;
            //if enemy is further from end, update
            if (currentDistance < furthestDistance)
            {
                furthestDistance = currentDistance;
                script.currentTarget = enemy;
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
