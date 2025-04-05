using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SOEnemyDisplay : MonoBehaviour
{
    public gameControllerScript gameController;
    public sliderDisplayScript sliderDisplay;
    
    public SOEnemy enemy;

    private SpriteRenderer sR;

    public float speed;
    public float health;
    public int damageToHP;

    public float hitRadius;

    public float distanceToCheckpoint;
    public int checkpointNum;
    public Vector3 checkpointPos;
    public List<GameObject> checkpointList;

    public bool reachedEnd;

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerScript.instance;
        sliderDisplay = transform.GetChild(0).GetComponent<sliderDisplayScript>();
        sR = GetComponent<SpriteRenderer>();
        sR.sprite = enemy.image;

        speed = enemy.speed;
        health = enemy.health;
        damageToHP = enemy.damageToHP;
        hitRadius = enemy.hitRadius;
        GetComponent<CircleCollider2D>().radius = hitRadius;

        checkpointList = gameController.waveManager.checkpointList;
        checkpointNum = 0;
        checkpointPos = checkpointList[checkpointNum].transform.position;

        sliderDisplay.updateDisplay(health, enemy.health);
    }

    void Update()
    {
        distanceToCheckpoint = Vector3.Distance(transform.position, checkpointPos);
        Vector3 direction = checkpointPos - transform.position;
        //find the direction from point A to point B via Vector subtraction
        transform.position += direction.normalized * speed * gameController.timeManager.currentTime / 1000;
        if (direction.x > 0)
        {
            sR.flipX = false;
        }
        else
        {
            sR.flipX = true;
        }
        if (distanceToCheckpoint <= 0.5)
        {
            //if not reached end, go to next checkpoint
            if (checkpointNum < checkpointList.Count)
            {
                checkpointPos = checkpointList[checkpointNum].transform.position;
            }
            //if each end, do stuff
            else
            {
                reachedEnd = true;
                gameController.waveManager.enemyDied();
                gameController.resourceManager.removeEnemy(this);
                gameController.resourceManager.changeHealth(enemy.damageToHP * -1);
                Destroy(gameObject);
            }
            checkpointNum += 1;
        }
        //if die, die
        if (health <= 0)
        {
            gameController.waveManager.enemyDied();
            gameController.resourceManager.changeGold(enemy.goldDrop);
            gameController.resourceManager.removeEnemy(this);
            Destroy(gameObject);
        }
    }
}
