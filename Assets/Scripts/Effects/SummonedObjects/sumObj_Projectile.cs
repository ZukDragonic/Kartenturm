using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sumObj_Projectile : sumObj_Base
{
    public baseTowerScript parentTower;
    public Vector3 target;

    void Start()
    {
        damage = parentTower.attackValue;
        pierce = parentTower.projectilePierce;
        effects = SOCE_Parent.CreateInstance(parentTower.currentEffect);

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
            hitEnemy.health -= damage;
            hitEnemy.sliderDisplay.updateDisplay(hitEnemy.health, hitEnemy.enemy.health);
            pierce -= 1;
            if (pierce <= 0)
            {
                effects.OnODestroyEffect(this as sumObj_Base);
                Destroy(gameObject);
            }
        }
    }
}