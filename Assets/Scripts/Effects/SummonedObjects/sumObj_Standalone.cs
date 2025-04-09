using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sumObj_Standalone : sumObj_Base
{
    public sumObj_Projectile parentProjectile;
    // Start is called before the first frame update
    void Start()
    {
        damage = parentProjectile.damage;
        pierce = parentProjectile.pierce;
        effects = SOCE_Parent.CreateInstance(effects);
        effects.OnOSummonEffect(this as sumObj_Base);
    }
    // Update is called once per frame
    void Update()
    {
        effects.OPassiveEffect(this as sumObj_Base);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
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
