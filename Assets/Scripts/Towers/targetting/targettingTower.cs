using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargettingTower", menuName = "Targetting/Tower")]
public class targettingTower : targettingBase
{
    public override void EnterCheck(baseTowerScript script)
    {

    }
    public override void UpdateCheck(baseTowerScript script)
    {
        List<baseTowerScript> allTower = new List<baseTowerScript> (gameControllerScript.instance.resourceManager.allTower);
        allTower.Remove(script);
        //some thing that remove towers that are already buffed/affected
        float closest = 0;
        GameObject currentTarget = null;
        if (allTower.Count > 0)
        {
            closest = Vector3.Distance(script.transform.position, allTower[0].transform.position);
            currentTarget = allTower[0].gameObject;
        }
        foreach (baseTowerScript tower in allTower)
        {
            float currentDistance = Vector3.Distance(script.transform.position, tower.transform.position);
            if (currentDistance < closest)
            {
                currentTarget = tower.gameObject;
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
        return "Tower";
    }
}