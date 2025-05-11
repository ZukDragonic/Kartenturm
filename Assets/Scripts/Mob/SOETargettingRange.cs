using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTargettingRange", menuName = "Enemy/Targetting/Range")]
public class SOETargettingRange : SOETargettingBase
{
    public float range;
    public override List<baseTowerScript> ChangeTargetting(SOEnemyDisplay enemyScript)
    {
        List<baseTowerScript> towerList = gameControllerScript.instance.resourceManager.aliveTower;

        List<baseTowerScript> returnList = new List<baseTowerScript>();

        foreach (baseTowerScript tower in towerList)
        {
            float distance = Vector3.Distance(tower.transform.position, enemyScript.transform.position);
            if (distance < range)
            {
                returnList.Add(tower);
            }
        }
        return returnList;
    }
}
