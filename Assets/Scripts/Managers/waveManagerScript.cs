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

    public List<SOEnemyDisplay> aliveEnemies;
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
        if (currentWave == null)
        {
            Debug.Log("YOU ENTERED AN INVALID WAVE");
            return;
        }
        StartCoroutine(waveManager(currentWave));
    }

    public void enemyDied()
    {
        enemyAlive -= 1;
        if (!isWaveSpawning)
        {
            if (enemyAlive == 0)
            {
                gameController.resourceManager.changeGold(currentWave.endGold);
                gameController.FlagTrigger(currentWave.ID);
                foreach (baseTowerScript tower in gameController.resourceManager.aliveTower)
                {
                    tower.waveEndAction();
                }
                //if not autostart, update gameState to a semi-paused state
                if (!gameController.FlagInfo(20001))
                {
                    gameController.UpdateGameState(GameState.BetweenWave);
                }
            }
        }
    }

    IEnumerator waveManager(Wave wave)
    {
        yield return null;
        gameController.resourceManager.updateWave(currentWave.waveNum);
        while (gameController.state != GameState.Wave)
        {
            yield return null;
        }
        foreach (baseTowerScript tower in gameController.resourceManager.aliveTower)
        {
            tower.waveStartAction();
        }
        enemyAlive = 0;
        isWaveSpawning = true;
        for (int i = 0; i < wave.spawnOrder.Length; i++)
        {
            for (int k = 0; k < wave.spawnOrder[i].repeat; k++)
            {
                float relativeDelay = wave.spawnOrder[i].delay * Application.targetFrameRate;
                int currentFrame = 0;
                while (currentFrame < relativeDelay)
                {
                    currentFrame += gameController.timeManager.currentTime;
                    yield return null;
                }
                GameObject current = Instantiate(enemyPrefab, new Vector3(checkpointList[0].transform.position.x - 0.1f,
                    checkpointList[0].transform.position.y - 0.1f, 0), Quaternion.identity);
                SOEnemyDisplay eScript = current.GetComponent<SOEnemyDisplay>();
                eScript.enemy = findEnemy(wave.spawnOrder[i].enemyID);
                gameController.resourceManager.aliveEnemy.Add(eScript);
                enemyAlive += 1;
                aliveEnemies.Add(eScript);
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
