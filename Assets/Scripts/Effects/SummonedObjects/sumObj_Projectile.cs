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
        effects = parentTower.baseCard.effects;

        StartCoroutine(WaitForStart());
    }
    IEnumerator WaitForStart()
    {
        yield return null;
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