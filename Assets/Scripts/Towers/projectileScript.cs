using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public baseTowerScript parentTower;
    public Vector3 target;

    private float speed;
    private float damage;
    private int pierce;
    // Start is called before the first frame update
    void Start()
    {
        damage = parentTower.attackValue;
        pierce = parentTower.projectilePierce;

        parentTower.baseCard.effects.OnPSummonEffect(this);
    }

    // Update is called once per frame
    void Update()
    {
        parentTower.baseCard.effects.PPassiveEffect(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            parentTower.baseCard.effects.OnPHitEffect(this);
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
    void OnDestroy()
    {
        parentTower.baseCard.effects.OnPDestroyEffect(this);
    }
}