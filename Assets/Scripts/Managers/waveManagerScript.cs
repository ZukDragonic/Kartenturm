using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class waveManagerScript : MonoBehaviour
{
    public gameControllerScript gameController;

    public SOEnemy[] allEnemies;

    public Wave[] waveList;
    public Wave currentWave;
    public int enemyAlive;
    private bool isWaveSpawning;

    private Wave freeplayEnemySpawnList;
    private int freeplaySpawnTotal;
    private int freeplaySpawnMaxRep;
    private int freeplaySpawnDelay;
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
                for (int k = 0; k < currentRoute.childCount; k++)
                {
                    routeList[i].checkpointList.Add(currentRoute.GetChild(k).gameObject);
                }
            }
        }

        currentFreeplayWave = null;
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
        freeplaySpawnTotal = 0;
        foreach (EnemyAndDelay enemy in currentWave.spawnOrder)
        {
            freeplaySpawnTotal += enemy.repeat * findEnemy(enemy.enemyID).spawnValue;
        }
        freeplaySpawnTotal = Mathf.FloorToInt(freeplaySpawnTotal * 1.1f);
        freeplaySpawnMaxRep = Mathf.FloorToInt(freeplaySpawnTotal / 100);
        freeplaySpawnDelay = Mathf.FloorToInt(1 / Mathf.Sqrt(freeplaySpawnTotal));


        //sets the wave of the spawner
        //if freeplayEnemySpawnList has no enemies in it, the spawnable enemies haven't been initialized
        //Therefore, the list where enemy can spawn in
        if (freeplayEnemySpawnList == null)
        {
            Debug.Log(3);
            //sets seed
            setSeed = new System.Random(freeplaySeed.GetHashCode());
            //sets spawnable template
            freeplayEnemySpawnList = findWave(ID);

            List<EnemyAndDelay> tempSpawnList = new List<EnemyAndDelay>();

            //adds in spawnable enemies
            foreach (EnemyAndDelay enemy in findWave(ID).spawnOrder)
            {
                tempSpawnList.Add(enemy);
            }

            freeplayEnemySpawnList.spawnOrder = tempSpawnList.ToArray();
        }
        freeplayEnemySpawnList.waveNum = currentWave.waveNum + 1;

        currentFreeplayWave = new Wave();
        currentFreeplayWave.endGold = 0;
        currentFreeplayWave.waveNum = freeplayEnemySpawnList.waveNum;
        currentFreeplayWave.ID = freeplayEnemySpawnList.ID;

        List<EnemyAndDelay> tempCurrentSpawnList = new List<EnemyAndDelay>();
        while (freeplaySpawnTotal > 0)
        {
            int spawnedEnemyIndex = setSeed.Next(0, freeplayEnemySpawnList.spawnOrder.Length);
            int spawnRepeat = setSeed.Next(1, freeplaySpawnMaxRep);

            EnemyAndDelay foundEnemy = new EnemyAndDelay();
            foundEnemy.enemyID = freeplayEnemySpawnList.spawnOrder[spawnedEnemyIndex].enemyID;
            foundEnemy.delay = freeplayEnemySpawnList.spawnOrder[spawnedEnemyIndex].delay * freeplaySpawnDelay;
            foundEnemy.repeat = spawnRepeat;

            int spawnPower = findEnemy(foundEnemy.enemyID).spawnValue * spawnRepeat;
            if (spawnPower < freeplaySpawnTotal)
            {
                tempCurrentSpawnList.Add(foundEnemy);
                freeplaySpawnTotal -= spawnPower;
            }
            else
            {
                int enemySpawnRepeat = Mathf.FloorToInt((float)freeplaySpawnTotal / (float)findEnemy(foundEnemy.enemyID).spawnValue + 1);
                freeplaySpawnTotal = 0;
            }
        }
        currentFreeplayWave.spawnOrder = tempCurrentSpawnList.ToArray();

        currentWave = currentFreeplayWave;
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

        int routeTracker = 0;
        int routeCount = routeList.Count;

        for (int i = 0; i < wave.spawnOrder.Length; i++)
        {
            for (int k = 0; k < wave.spawnOrder[i].repeat; k++)
            {
                float relativeDelay = wave.spawnOrder[i].delay * Application.targetFrameRate;
                int currentFrame = 0;

                //makes the first enemy spawned 0 second
                if (i == 0 && k == 0)
                {
                    currentFrame = (int)relativeDelay;
                }
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
