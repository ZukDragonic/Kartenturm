using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class sumObj_Minion : sumObj_Base
{
    private gameControllerScript gameController;
    public baseTowerScript parentTower;

    public SOSumObj baseSumObj;
    public CircleCollider2D hitbox;

    public SpriteRenderer sR;
    public Animator anim;
    private AnimatorOverrideController aoc;

    //pierce is treated as current hp, while damage is max health
    public int pierce;

    public SOEnemyDisplay hitEnemy;

    public float speed;
    public float hitRadius;
    public sliderDisplayScript sliderDisplay;
    private float distanceToCheckpoint;
    //public float totalRemainingDistance;
    public int checkpointNum;
    private Vector3 checkpointPos;
    public List<GameObject> checkpointList;

    void Start()
    {
        gameController = gameControllerScript.instance;

        hitbox.radius = (float)baseSumObj.hitRadius / 100;
        sR.sprite = baseSumObj.image;

        damage = baseSumObj.damage;
        pierce = damage;

        if (baseSumObj.animClip != null)
        {
            aoc = new AnimatorOverrideController(anim.runtimeAnimatorController);
            aoc["SumObjAnim_Base"] = baseSumObj.animClip;
            anim.runtimeAnimatorController = aoc;

            anim.speed = gameControllerScript.instance.timeManager.currentTime;
        }
        else
        {
            anim.enabled = false;
        }

        effects = SOCE_Parent.CreateInstance(baseSumObj.effect);

        //checkpointList and checkpointNum is set in SOCE_SummonMinion
        checkpointPos = checkpointList[checkpointNum].transform.position;
        sliderDisplay.updateDisplay(pierce, damage);
        if (hitRadius <= 0)
        {
            sliderDisplay.gameObject.SetActive(false);
        }

        effects.OnOSummonEffect(this as sumObj_Base);
    }

    // Update is called once per frame
    void Update()
    {
        effects.OPassiveEffect(this as sumObj_Base);

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
            if (checkpointNum != 0)
            {
                checkpointPos = checkpointList[checkpointNum].transform.position;
            }
            //if each end, do stuff
            else
            {
                effects.OnODestroyEffect(this as sumObj_Base);
                Destroy(gameObject);
            }
            checkpointNum -= 1;
        }

        //removes because enemies doesn't target summons
       /*float tempTRD = distanceToCheckpoint;
        for (int i = checkpointNum + 1; i < checkpointList.Count - 1; i++)
        {
            tempTRD += (Vector3.Distance(checkpointList[i].transform.position, checkpointList[i + 1].transform.position));
        }
        totalRemainingDistance = tempTRD;*/
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            damage = pierce;
            hitEnemy = collision.gameObject.GetComponent<SOEnemyDisplay>();
            pierce -= (int)hitEnemy.health;
            effects.OnOHitEffect(this as sumObj_Base);
            hitEnemy.enemyHit(this);

            sliderDisplay.updateDisplay(pierce, damage);

            if (pierce <= 0)
            {
                effects.OnODestroyEffect(this as sumObj_Base);
                Destroy(gameObject);
            }
        }
    }
}
