using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectSummonMinion", menuName = "Card/Effect/SummonMinion")]
public class SOCE_SummonMinion : SOC_Effect
{
    public GameObject summonedPrefab;

    //specifically for minion
    public SOSumObj summonedObject;

    public bool copyEnemyOnSpawn;

    public override void ApplyEEffect(SOEnemyDisplay script)
    {
        //maybe migrate enemy summon over
        /*for (int i = 0; i < sumNum; i++)
        {
            Vector3 sumPos = script.gameObject.transform.position;
            sumPos -= (script.transform.position - script.checkpointPos).normalized * i;
            GameObject current = Instantiate(summonedPrefab, sumPos, Quaternion.identity, script.transform.parent);
            SOEnemyDisplay summonedScript = current.GetComponent<SOEnemyDisplay>();
            if (summonedEnemy.Count > 1)
            {
                summonedScript.enemy = summonedEnemy[Random.Range(0, summonedEnemy.Count)];
            }
            else
            {
                summonedScript.enemy = summonedEnemy[0];
            }
            summonedScript.checkpointNum = script.checkpointNum;
            summonedScript.checkpointList = script.checkpointList;
            gameControllerScript.instance.resourceManager.addEnemy(summonedScript);
            gameControllerScript.instance.waveManager.enemyAlive += 1;
        }*/
    }
    public override void ApplyOMEffect(sumObj_Minion script)
    {
        GameObject current = Instantiate(summonedPrefab);
        sumObj_Minion summonedScript = current.GetComponent<sumObj_Minion>();
        summonedScript.baseSumObj = summonedObject;
        summonedScript.parentTower = script.parentTower;

        waveManagerScript waveManager = gameControllerScript.instance.waveManager;
        Route routeSelected = waveManager.routeList[Random.Range(0, waveManager.routeList.Count)];

        summonedScript.checkpointList = routeSelected.checkpointList;

        int startingCheckpoint = routeSelected.checkpointList.Count - 1;
        if (copyEnemyOnSpawn)
        {
            startingCheckpoint = script.hitEnemy.checkpointNum - 2;
            current.transform.position = script.hitEnemy.transform.position;

            summonedScript.baseSumObj = SOSumObj.ConvertEnemyToMinion(script.hitEnemy.enemy, summonedScript.baseSumObj);
        }
        else
        {
            current.transform.position = routeSelected.checkpointList[startingCheckpoint].transform.position;
        }

        summonedScript.checkpointNum = startingCheckpoint;
    }
    public override void ApplyTEffect(baseTowerScript script)
    {
        //must be spawn at end for this
        GameObject current = Instantiate(summonedPrefab);
        sumObj_Minion summonedScript = current.GetComponent<sumObj_Minion>();

        summonedScript.baseSumObj = summonedObject;
        summonedScript.parentTower = script;

        waveManagerScript waveManager = gameControllerScript.instance.waveManager;
        Route routeSelected = waveManager.routeList[Random.Range(0, waveManager.routeList.Count)];

        summonedScript.checkpointList = routeSelected.checkpointList;

        int startingCheckpoint = routeSelected.checkpointList.Count - 1;

        current.transform.position = routeSelected.checkpointList[startingCheckpoint].transform.position;
        summonedScript.checkpointNum = startingCheckpoint;
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        GameObject current = Instantiate(summonedPrefab);
        sumObj_Minion summonedScript = current.GetComponent<sumObj_Minion>();
        summonedScript.baseSumObj = summonedObject;
        summonedScript.parentTower = script.parentTower;

        waveManagerScript waveManager = gameControllerScript.instance.waveManager;
        Route routeSelected = waveManager.routeList[Random.Range(0, waveManager.routeList.Count)];

        summonedScript.checkpointList = routeSelected.checkpointList;

        int startingCheckpoint = routeSelected.checkpointList.Count - 1;
        if (copyEnemyOnSpawn)
        {
            startingCheckpoint = script.hitObject.checkpointNum - 2;
            current.transform.position = script.hitObject.transform.position;

            summonedScript.baseSumObj = SOSumObj.ConvertEnemyToMinion(script.hitObject.enemy, summonedScript.baseSumObj);
        }
        else
        {
            current.transform.position = routeSelected.checkpointList[startingCheckpoint].transform.position;
        }

        summonedScript.checkpointNum = startingCheckpoint;
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        GameObject current = Instantiate(summonedPrefab);
        sumObj_Minion summonedScript = current.GetComponent<sumObj_Minion>();
        summonedScript.baseSumObj = summonedObject;
        summonedScript.parentTower = script.parentTower;

        waveManagerScript waveManager = gameControllerScript.instance.waveManager;
        Route routeSelected = waveManager.routeList[Random.Range(0, waveManager.routeList.Count)];

        summonedScript.checkpointList = routeSelected.checkpointList;

        int startingCheckpoint = routeSelected.checkpointList.Count - 1;
        if (copyEnemyOnSpawn)
        {
            startingCheckpoint = script.hitEnemy.checkpointNum - 2;
            current.transform.position = script.hitEnemy.transform.position;

            summonedScript.baseSumObj = SOSumObj.ConvertEnemyToMinion(script.hitEnemy.enemy, summonedScript.baseSumObj);
        }
        else
        {
            current.transform.position = routeSelected.checkpointList[startingCheckpoint].transform.position;
        }

        summonedScript.checkpointNum = startingCheckpoint;
    }
    public override void ApplyBEffect()
    {
        GameObject current = Instantiate(summonedPrefab);
        sumObj_Minion summonedScript = current.GetComponent<sumObj_Minion>();

        waveManagerScript waveManager = gameControllerScript.instance.waveManager;
        Route routeSelected = waveManager.routeList[Random.Range(0, waveManager.routeList.Count)];

        summonedScript.checkpointList = routeSelected.checkpointList;

        int startingCheckpoint = routeSelected.checkpointList.Count - 1;

        current.transform.position = routeSelected.checkpointList[startingCheckpoint].transform.position;
        summonedScript.checkpointNum = startingCheckpoint;
    }
}
