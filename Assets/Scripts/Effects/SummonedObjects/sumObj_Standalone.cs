using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sumObj_Standalone : sumObj_Base
{
    //the effects should go into the base effect since it swaps when instantiated
    //Any effect that needs to be inherited from parent needs to be placed in the base

    public sumObj_Projectile parentProjectile;
    void Start()
    {
        damage = parentProjectile.damage;
        pierce = parentProjectile.pierce;
        effects = SOCE_Parent.MergeInstances(effects, parentProjectile.effects);
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
            hitEnemy = collision.gameObject.GetComponent<SOEnemyDisplay>();
            effects.OnOHitEffect(this as sumObj_Base);
            SOEnemyDisplay script = collision.gameObject.GetComponent<SOEnemyDisplay>();
            script.health -= damage;
            script.sliderDisplay.updateDisplay(script.health, script.enemy.health);
            pierce -= 1;
            if (pierce <= 0)
            {
                effects.OnODestroyEffect(this as sumObj_Base);
                Destroy(gameObject);
            }
        }
    }
}
