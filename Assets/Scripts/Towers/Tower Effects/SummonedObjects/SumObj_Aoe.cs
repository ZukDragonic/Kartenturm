using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumObj_Aoe : SumObj_Base
{
    private float damage;
    private int pierce;

    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        damage = parentTower.attackValue;
        pierce = 999;
        Destroy(gameObject, duration);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            SOEnemyDisplay script = collision.gameObject.GetComponent<SOEnemyDisplay>();
            script.health -= damage;
            script.sliderDisplay.updateDisplay(script.health, script.enemy.health);
            pierce -= 1;
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
