using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManagerScript : MonoBehaviour
{
    public gameControllerScript gameController;

    public SOEnemy[] allEnemies;

    public Wave[] waveList;
    public Wave currentWave;
    public int enemyAlive;
    private bool isWaveSpawning;

    private List<int> freeplayEnemySpawnList;
    private int freeplaySpawnTotal;
    private int freeplaySpawnMaxRep;
    private Wave currentFreeplayWave;
    public string freeplaySeed;
    private System.Random setSeed; 

    public GameObject enemyPrefab;

    private GameObject checkpointParent;
    public List<Route> routeList;
    //all maps must be 25x18

    public GameObject gameLossPrefab;
    public GameObject gameWinPrefab;

    public void StartAction()
    {
        gameController = gameControllerScript.instance;

        gameController.FlagTrigger(19001);

        StopAllCoroutines();

        checkpointParent = GameObject.Find("Checkpoint Parent");
        if (checkpointParent != null)
        {

            routeList = new List<Route>();
            for (int i = 0; i < checkpointParent.transform.childCount; i++)
            {
                routeList.Add(new Route());
                routeList[i].checkpointList = new List<GameObject>();
                Transform currentRoute = checkpointParent.transform.GetChild(i);
                for (int k = 0; k < currentRoute.childCount;k++)
                {
                    routeList[i].checkpointList.Add(currentRoute.GetChild(k).gameObject);
                }
            }
        }

        freeplayEnemySpawnList = null;
        freeplaySpawnTotal = 0;
        freeplaySpawnMaxRep = 0;
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

    public void spawnFreePlayWave(int ID)
    {
        //if freeplayEnemySpawnList has no enemies in it, the spawnable enemies haven't been initialized
        //Therefore, initialize every variable
        if (freeplayEnemySpawnList == null)
        {
            foreach (EnemyAndDelay enemy in currentWave.spawnOrder)
            {
                freeplaySpawnTotal += enemy.repeat * findEnemy(enemy.enemyID).spawnValue;
            }

            freeplayEnemySpawnList = new List<int>();
            Wave tempWave = findWave(ID);

            //adds in spawnable enemies
            foreach (EnemyAndDelay enemy in tempWave.spawnOrder)
            {
                freeplayEnemySpawnList.Add(enemy.enemyID);
            }

            setSeed = new System.Random(freeplaySeed.GetHashCode());
        }
        currentFreeplayWave = new Wave();
        currentFreeplayWave.endGold = 0;
        currentFreeplayWave.waveNum = currentWave.waveNum + 1;

        while (freeplaySpawnTotal > 0)
        {
            int spawnedEnemyIndex = setSeed.Next(0, freeplayEnemySpawnList.Count);
            SOEnemy foundEnemy = findEnemy(freeplayEnemySpawnList[spawnedEnemyIndex]);
            if (foundEnemy.spawnValue > freeplaySpawnTotal)
            {

            }

            //currentFreeplayWave.spawnOrder
        }

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

        int routeTracker = 0;
        int routeCount = routeList.Count;

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

                List<GameObject> spawnedCheckpoint = routeList[routeTracker % routeCount].checkpointList;
                routeTracker += 1;

                GameObject current = Instantiate(enemyPrefab, new Vector3(spawnedCheckpoint[0].transform.position.x - 0.1f,
                    spawnedCheckpoint[0].transform.position.y - 0.1f, 0), Quaternion.identity);
                SOEnemyDisplay eScript = current.GetComponent<SOEnemyDisplay>();
                eScript.enemy = findEnemy(wave.spawnOrder[i].enemyID);
                eScript.checkpointList = spawnedCheckpoint;
                enemyAlive += 1;
                gameController.resourceManager.addEnemy(eScript);
            }
        }
        isWaveSpawning = false;
    }

    public void GameLostMenu()
    {
        Instantiate(gameLossPrefab, gameController.allCanvas[3].transform);
    }

    public void GameWinMenu()
    {
        Instantiate(gameWinPrefab, gameController.allCanvas[3].transform);
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
