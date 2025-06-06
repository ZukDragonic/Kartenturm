using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class destroyTowerScript : MonoBehaviour
{
    public baseTowerScript parentTower;

    void destroyTower()
    {
        //remove comment for limited placement
        /*parentTower.transform.parent.GetComponent<placeableAreaScript>().isPlaceable = true;
        parentTower.transform.parent.GetComponent<CircleCollider2D>().enabled = true;*/
        gameControllerScript.instance.resourceManager.removeTower(parentTower);
        Destroy(parentTower.gameObject);
    }
}
