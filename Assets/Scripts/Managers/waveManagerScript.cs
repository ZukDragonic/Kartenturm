using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameControllerScript;

public class waveManagerScript : MonoBehaviour
{
    public gameControllerScript gameController;

    public SOEnemy[] allEnemies;

    public Wave[] waveList;
    private Wave currentWave;
    private int enemyAlive;
    private bool isWaveSpawning;

    public GameObject enemyPrefab;

    private GameObject checkpointParent;
    public List<GameObject> checkpointList;
    //all maps must be 25x18
    void Start()
    {
        gameController = gameControllerScript.instance;

        checkpointParent = GameObject.Find("Checkpoint Parent");
        for (int i = 0; i < checkpointParent.transform.childCount; i++)
        {
            checkpointList.Add(checkpointParent.transform.GetChild(i).gameObject);
        }
    }

    public void spawnWave(int ID)
    {
        currentWave = findWave(ID);
        StartCoroutine(waveManager(currentWave));
    }

    public void enemyDied()
    {
        enemyAlive -= 1;
        if (!isWaveSpawning)
        {
            if (enemyAlive == 0)
            {
                gameController.FlagTrigger(currentWave.ID);
                //if not autostart, update gameState to a semi-paused state
                if (!gameController.FlagInfo(20001))
                {
                    gameController.UpdateGameState(GameState.BetweeWave);
                }
            }
        }
    }

    IEnumerator waveManager(Wave wave)
    {
        Debug.Log("hi");
        yield return null;
        while (gameController.state == GameState.BetweeWave || gameController.state == GameState.Placing)
        {
            yield return null;
        }
        
        enemyAlive = 0;
        isWaveSpawning = true;
        for (int i = 0; i < wave.spawnOrder.Length; i++)
        {
            for (int k = 0; k < wave.spawnOrder[i].repeat; k++)
            {
                yield return new WaitForSeconds(wave.spawnOrder[i].delay);
                GameObject current = Instantiate(enemyPrefab, new Vector3(checkpointList[0].transform.position.x - 0.1f,
                    checkpointList[0].transform.position.y - 0.1f, 0), Quaternion.identity);
                current.GetComponent<SOEnemyDisplay>().enemy = findEnemy(wave.spawnOrder[i].enemyID);
                enemyAlive += 1;
            }
        }
        isWaveSpawning = false;
    }

    SOEnemy findEnemy(int ID)
    {
        foreach (SOEnemy enemy in allEnemies)
        {
            if (enemy.ID == ID)
            {
                return enemy;
            }
        }
        return allEnemies[0];
    }

    Wave findWave(int ID)
    {
        foreach (Wave wave in waveList)
        {
            if (wave.ID == ID)
            {
                return wave;
            }
        }
        return null;
    }
}
