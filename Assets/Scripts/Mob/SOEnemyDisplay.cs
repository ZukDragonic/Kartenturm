using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class SOEnemyDisplay : sumObj_Base
{
    public gameControllerScript gameController;
    public sliderDisplayScript sliderDisplay;
    
    public SOEnemy enemy;

    public SpriteRenderer sR;
    public Animator anim;
    private AnimatorOverrideController aoc;

    public float speed;
    public float health;
    public int hitDamage; //the damage the enemy takes when hit by tower. purely here for effect modification

    public float hitRadius;

    public float distanceToCheckpoint;
    public int checkpointNum;
    public Vector3 checkpointPos;
    public List<GameObject> checkpointList;

    public bool reachedEnd;

    private float attackTimer;
    private List<baseTowerScript> affectedTowers;
    public baseTowerScript currentAffectedTower;

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerScript.instance;
        sliderDisplay = transform.GetChild(0).GetComponent<sliderDisplayScript>();
        sR.sprite = enemy.image;

        if (enemy.animClip != null)
        {
            aoc = new AnimatorOverrideController(anim.runtimeAnimatorController);
            aoc["E_AnimMove"] = enemy.animClip;
            anim.runtimeAnimatorController = aoc;
        }
        else
        {
            anim.enabled = false;
        }

        speed = enemy.speed;
        health = enemy.health;
        hitRadius = enemy.hitRadius;
        effects = SOCE_Parent.CreateInstance(enemy.effects);
        GetComponent<CircleCollider2D>().radius = hitRadius / 100;

        checkpointList = gameController.waveManager.checkpointList;
        checkpointNum = 0;
        checkpointPos = checkpointList[checkpointNum].transform.position;

        sliderDisplay.updateDisplay(health, enemy.health);

        effects.OnSummonEffect(this);

        StartCoroutine(AttackCycle());
    }

    void Update()
    {
        effects.PassiveEffect(this);
        anim.speed = gameController.timeManager.currentTime;

        distanceToCheckpoint = Vector3.Distance(transform.position, checkpointPos);
        //fix bug where when enemies too fast, it'd jump past the checkpoint
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
                effects.OnDestroyEffect(this);

                reachedEnd = true;
                gameController.waveManager.enemyDied();
                gameController.resourceManager.changeHealth(enemy.damageToHP * -1);
                gameController.resourceManager.removeEnemy(this);
            }
            checkpointNum += 1;
        }
        //if die, die
        if (health <= 0)
        {
            effects.OnDestroyEffect(this);

            gameController.waveManager.enemyDied();
            gameController.resourceManager.changeGold(enemy.goldDrop);
            gameController.resourceManager.removeEnemy(this);
        }
    }

    IEnumerator AttackCycle()
    {
        yield return null;

        bool hasAttack = enemy.attacks.Length > 0;
        attackTimer = 0;
        //attackTimer increases relative to the fake frame rate
        while (hasAttack)
        {
            EnemyAttack currentAttack = enemy.attacks[Random.Range(0, enemy.attacks.Length)];

            if (currentAttack.anim != null)
            {
                if (aoc != null)
                {
                    aoc["E_AnimAttack"] = currentAttack.anim;
                    anim.Play("Temporary", 0);
                }
            }
            
            if (!currentAttack.moveDuringAttack)
            {
                speed = 0;
            }

            while (attackTimer < currentAttack.startLag)
            {
                attackTimer += 1f / (float)Application.targetFrameRate * (float)gameController.timeManager.currentTime;
                yield return null;
            }
            attackTimer -= currentAttack.startLag;


            if (currentAttack.target != null)
            {
                affectedTowers = currentAttack.target.ChangeTargetting(this);
                foreach (baseTowerScript tower in affectedTowers)
                {
                    currentAffectedTower = tower;
                    foreach (SOC_Effect effect in currentAttack.appliedEffect)
                    {
                        effect.ApplyEffect(this);
                    }
                }
            }
            else
            {
                foreach (SOC_Effect effect in currentAttack.appliedEffect)
                {
                    effect.ApplyEffect(this);
                }
            }


            while (attackTimer < currentAttack.endLag)
            {
                attackTimer += 1f / (float)Application.targetFrameRate * (float)gameController.timeManager.currentTime;
                yield return null;
            }
            attackTimer -= currentAttack.endLag;

            if (currentAttack.endEffect != null)
            {
                foreach (SOC_Effect effect in currentAttack.endEffect)
                {
                    effect.ApplyEffect(this);
                }
            }

            if (!currentAttack.moveDuringAttack)
            {
                speed = enemy.speed;
            }

            while (attackTimer < currentAttack.timeTillNextAttack)
            {
                attackTimer += 1f / (float)Application.targetFrameRate * (float)gameController.timeManager.currentTime;
                yield return null;
            }
            attackTimer -= currentAttack.timeTillNextAttack;
        }
    }

    public void enemyHit(int damage)
    {
        hitDamage = damage;
        effects.OnHitEffect(this);

        health -= hitDamage;

        sliderDisplay.updateDisplay(health, enemy.health);
    }
}
