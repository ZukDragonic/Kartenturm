using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sumObj_Projectile : sumObj_Base
{
    public baseTowerScript parentTower;
    public Vector3 target;

    public int pierce;

    public SOEnemyDisplay hitEnemy;
    void Start()
    {
        damage = parentTower.damage;
        pierce = parentTower.pierce;
        effects = SOCE_Parent.CreateInstance(parentTower.effects);

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
            hitEnemy = collision.gameObject.GetComponent<SOEnemyDisplay>();
            effects.OnOHitEffect(this as sumObj_Base);
            hitEnemy.enemyHit(this);

            pierce -= 1;
            if (pierce <= 0)
            {
                effects.OnODestroyEffect(this as sumObj_Base);
                Destroy(gameObject);
            }
        }
    }
}