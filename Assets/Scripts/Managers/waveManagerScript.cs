using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameControllerScript;

public class waveManagerScript : MonoBehaviour
{
    public gameControllerScript gameController;

    public SOEnemy[] allEnemies;

    public Wave[] waveList;
    public Wave currentWave;
    public int enemyAlive;
    private bool isWaveSpawning;

    public GameObject enemyPrefab;

    private GameObject checkpointParent;
    public List<GameObject> checkpointList;
    //all maps must be 25x18

    public void StartAction()
    {
        gameController = gameControllerScript.instance;

        StopAllCoroutines();

        checkpointParent = GameObject.Find("Checkpoint Parent");
        if (checkpointParent != null)
        {
            checkpointList = new List<GameObject>();
            for (int i = 0; i < checkpointParent.transform.childCount; i++)
            {
                checkpointList.Add(checkpointParent.transform.GetChild(i).gameObject);
            }
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
                //if not autostart or shop wave, update gameState to a semi-paused state
                if (!gameController.FlagInfo(20001))
                {
                    gameController.timeManager.buttonChange(-1);
                    gameController.UpdateGameState(GameState.BetweenWave);
                }

                //changes to next wave
                gameController.resourceManager.changeGold(currentWave.endGold);
                gameController.FlagTrigger(currentWave.ID);
                foreach (baseTowerScript tower in gameController.resourceManager.aliveTower)
                {
                    tower.waveEndAction();
                }
            }
        }
    }

    IEnumerator waveManager(Wave wave)
    {
        yield return null;
        gameController.resourceManager.updateWave(currentWave.waveNum);
        //wait for gameState is wait if not autostart
        //wait for prev gamestate to not be betweenwave if autostart
        while ((gameController.state != GameState.Wave && !gameController.FlagInfo(20001)) ||
            (gameController.prevState == GameState.BetweenWave && gameController.FlagInfo(20001)))
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
                enemyAlive += 1;
                gameController.resourceManager.addEnemy(eScript);
            }
        }
        isWaveSpawning = false;
    }

    public SOEnemy findEnemy(int ID)
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

    public Wave findWave(int ID)
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
