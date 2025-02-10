using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class targettingTower : targettingBase
{
    public override void EnterCheck(baseTowerScript script)
    {

    }
    public override void UpdateCheck(baseTowerScript script)
    {
        List<GameObject> allTower = new List<GameObject> (gameControllerScript.instance.resourceManager.allTower);
        allTower.Remove(script.gameObject);
        //some thing that remove towers that are already buffed/affected
        float closest = 0;
        GameObject currentTarget = null;
        if (allTower.Count > 0)
        {
            closest = Vector3.Distance(script.transform.position, allTower[0].transform.position);
            currentTarget = allTower[0];
        }
        foreach (GameObject tower in allTower)
        {
            float currentDistance = Vector3.Distance(script.transform.position, tower.transform.position);
            if (currentDistance < closest)
            {
                currentTarget = tower;
                closest = currentDistance;
            }
        }
        if (closest <= script.transform.localScale.x * (script.range / 10 / script.transform.localScale.x))
        {
            script.currentTarget = currentTarget;
        }
        else
        {
            script.currentTarget = null;
        }
    }
    public override void ExitCheck(baseTowerScript script)
    {
        script.currentTargetting = this;
    }
    public override string TargettingCheck()
    {
        return "Tower";
    }
}