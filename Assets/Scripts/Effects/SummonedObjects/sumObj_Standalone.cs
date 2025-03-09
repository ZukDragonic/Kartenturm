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
        StartCoroutine(WaitForStart());
    }
    IEnumerator WaitForStart()
    {
        yield return null;
        effects.OnPSummonEffect(this as sumObj_Base);
    }
    // Update is called once per frame
    void Update()
    {
        effects.PPassiveEffect(this as sumObj_Base);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            effects.OnPHitEffect(this as sumObj_Base);
            SOEnemyDisplay script = collision.gameObject.GetComponent<SOEnemyDisplay>();
            script.health -= damage;
            script.sliderDisplay.updateDisplay(script.health, script.enemy.health);
            pierce -= 1;
            if (pierce <= 0)
            {
                effects.OnPDestroyEffect(this as sumObj_Base);
                Destroy(gameObject);
            }
        }
    }
}
