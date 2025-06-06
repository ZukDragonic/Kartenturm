using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sumObj_Standalone : sumObj_Base
{
    //the effects should go into the base effect since it swaps when instantiated
    //Any effect that needs to be inherited from parent needs to be placed in the base

    public baseTowerScript parentTower;
    public sumObj_Projectile parentProjectile;

    public SOSumObj baseSumObj;

    public CircleCollider2D hitbox;
    public SpriteRenderer image;
    public Animator anim;

    public int pierce;

    public sumObj_Base hitObject;
    void Start()
    {
        hitbox.radius = (float)baseSumObj.hitRadius / 5f;
        image.sprite = baseSumObj.image;
        damage = baseSumObj.damage;
        pierce = baseSumObj.pierce;
        if (baseSumObj.anim != null)
        {
            anim.runtimeAnimatorController = baseSumObj.anim;
            anim.speed = gameControllerScript.instance.timeManager.currentTime;
        }
        effects = SOCE_Parent.CreateInstance(baseSumObj.effect);

        if (parentProjectile != null)
        {
            parentTower = parentProjectile.parentTower;
            damage += parentProjectile.damage;
            pierce += parentProjectile.pierce;
            effects = SOCE_Parent.MergeInstances(effects, parentProjectile.effects);
        }
        effects = SOCE_Parent.SwapTowerAndSumobj(effects);

        effects.OnOSummonEffect(this as sumObj_Base);
    }

    void Update()
    {
        effects.OPassiveEffect(this as sumObj_Base);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            hitObject = collision.gameObject.GetComponent<SOEnemyDisplay>();
            effects.OnOHitEffect(this as sumObj_Base);
            SOEnemyDisplay script = collision.gameObject.GetComponent<SOEnemyDisplay>();
            (hitObject as SOEnemyDisplay).enemyHit(this);

            pierce -= 1;
            if (pierce <= 0)
            {
                effects.OnODestroyEffect(this as sumObj_Base);
                Destroy(gameObject);
            }
        }
    }
}
