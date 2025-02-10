using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public baseTowerScript parentTower;

    private bool isBounce;
    private bool isArea;
    private int bounceNum;

    private float duration;
    private float speed;
    private float damage;
    private int pierce;
    // Start is called before the first frame update
    void Start()
    {
        speed = parentTower.baseCard.projectileSpeed;
        duration = parentTower.projectileDuration;
        bounceNum = parentTower.baseCard.projectileBounceNum;
        isBounce = parentTower.baseCard.isProjectileBounce;
        isArea = parentTower.baseCard.isAreaAttack;
        damage = parentTower.attackValue;
        pierce = parentTower.projectilePierce;

        StartCoroutine(projectileDuration());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += transform.right.normalized * speed * Time.deltaTime * 5;
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

    IEnumerator projectileDuration()
    {
        yield return new WaitForSeconds(duration);
        if (isArea)
        {
            //add area explosion thing
        }
        Destroy(gameObject);
    }
}
