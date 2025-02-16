using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;
public class baseTowerScript : MonoBehaviour
{
    public SOCTower baseCard;
    public List<SOCTower> stackedCards;
    public SOCTower placeHolderCard;

    private bool hasAttacked;

    private tCosmeticScript cosmetic;
    public bool isSelected;

    public GameObject towerMenuPrefabL;
    public GameObject towerMenuPrefabR;
    private static GameObject openedTowerMenu;

    private gameControllerScript gameController;

    public GameObject currentTarget;
    public List<GameObject> enemyInRange;

    public targettingBase currentTargetting;
    public targettingFirst targetFirst = new targettingFirst();
    public targettingLast targetLast = new targettingLast();
    public targettingStrong targetStrong =  new targettingStrong();
    public targettingWeak targetWeak = new targettingWeak();
    public targettingClose targetClose = new targettingClose();
    public targettingFar targetFar = new targettingFar();
    public targettingTower targetTower = new targettingTower();

    private bool targetAlly;
    private bool targetEnemy;
    private bool targetNone;
    public int targetChange;

    public float range;
    public float attackValue;
    public int projectilePierce;
    private int shotsPerAttack;
    private float secondsPerAttack;

    public GameObject baseProjectile;
   
    void Start()
    {
        cosmetic = GetComponent<tCosmeticScript>();
        cosmetic.showTower(baseCard);

        targetAlly = false;
        targetEnemy = false;
        targetNone = false;
        
        if (baseCard.targetting == targettingOption.Enemy)
        {
            currentTargetting = targetFirst;
            targetEnemy = true;
        }
        else if (baseCard.targetting == targettingOption.Ally)
        {
            currentTargetting = targetTower;
            targetAlly = true;
        }
        else if (baseCard.targetting == targettingOption.None)
        {
            targetNone = true;
        }

        range = baseCard.range;
        attackValue = baseCard.attackValue;
        projectilePierce = baseCard.projectilePierce;
        shotsPerAttack = baseCard.shotsPerAttack;
        secondsPerAttack = baseCard.secondsPerAttack;
        transform.GetChild(0).GetComponent<CircleCollider2D>().radius = range / 10 / transform.localScale.x / 3.125f;

        hasAttacked = false;

        enemyInRange = new List<GameObject>();

        stackedCards.Add(placeHolderCard);
        stackedCards.Add(placeHolderCard);
        stackedCards.Add(placeHolderCard);

        gameController = gameControllerScript.instance;
        gameController.resourceManager.addTower(gameObject);

        StartCoroutine(attackCycle());
    }
    void Update()
    {
        //hover highlight, click hightlight, and gui open/close
        //if mouse is over the tower
        if (gameController.mainInput_Ray.hit == gameObject)
        {
            if (!isSelected)
            {
                if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    cosmetic.HoverMat();
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (openedTowerMenu != null)
                        {
                            closeTowerMenu();
                        }
                        cosmetic.ClickMat();

                        openTowerMenu();
                        isSelected = true;
                    }
                }
            }
            else
            {
                if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    if (Input.GetMouseButtonDown(0))         
                    {
                        cosmetic.ResetMat();
                        closeTowerMenu();
                        isSelected = false;
                    }
                }
            }
        }
        else
        {
            if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                if (!isSelected)
                {
                    cosmetic.ResetMat();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemyInRange.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemyInRange.Remove(collision.gameObject);
        }
    }

    public void openTowerMenu()
    {
        openedTowerMenu = null;
        gameController.UpdateGameState(GameState.TowerMenu);

        if (Camera.main.WorldToViewportPoint(gameObject.transform.position).x < 0.5f)
        {
            openedTowerMenu = Instantiate(towerMenuPrefabR, gameController.topCanvas.transform);
        }
        else
        {
            openedTowerMenu = Instantiate(towerMenuPrefabL, gameController.topCanvas.transform);
        }
        openedTowerMenu.GetComponent<towerMenuDisplay>().parentTower = this;
        openedTowerMenu.GetComponentInChildren<slotParentScriptUGUI>().parentTower = this;
        openedTowerMenu.GetComponentInChildren<destroyTowerScript>().parentTower = this;

        cosmetic.rangeActivity(true, range);
    }
    public void closeTowerMenu()
    {
        gameController.UpdateGameState(GameState.Wave);
        Destroy(openedTowerMenu);
        openedTowerMenu = null;
        cosmetic.rangeActivity(false, range);
    }

    public void changeStats()
    {
        attackValue = baseCard.attackValue;
        foreach (var slot in stackedCards)
        {
            attackValue += slot.attackValue / 2 * baseCard.multiplier;
        }
        projectilePierce = baseCard.projectilePierce;
        foreach (var slot in stackedCards)
        {
            projectilePierce += (int)(slot.projectilePierce / 2 * baseCard.multiplier);
        }
        range = baseCard.range;
        foreach (var slot in stackedCards)
        {
            range += slot.range / 2 * baseCard.multiplier;
        }
        transform.GetChild(0).GetComponent<CircleCollider2D>().radius = range / 10 / transform.localScale.x / 3.125f;
    }

    public void changeTargetting(int change)
    {
        currentTargetting.ExitCheck(this);
    }

    IEnumerator attackCycle()
    {
        while (true)
        {
            while (!hasAttacked)
            {
                //check for target
                currentTargetting.UpdateCheck(this);

                if (currentTarget != null)
                {
                    Vector3 target = Vector3.zero;
                    //face target
                    if (targetEnemy || targetAlly)
                    {
                        target = currentTarget.transform.position;
                    }
                    else if (targetNone)
                    {
                        int ran = UnityEngine.Random.Range(0, 360);
                        float ranRadian = ran / 180 * 3.14f;
                        float xPos = Mathf.Cos(ranRadian);
                        float yPos = Mathf.Sin(ranRadian);
                        target = new Vector3(xPos, yPos, 0);
                        target += transform.position;
                    }
                    target.z = transform.position.z;
                    target = transform.position - target;
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.left,target);
                   // Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, target.z));

                    if (currentTarget.transform.position.x < transform.position.x)
                    {
                        cosmetic.flipSprite(true);
                    }
                    else
                    {
                        cosmetic.flipSprite(false);
                    }

                    //spawns attack
                    for (int i = 0; i < shotsPerAttack; i++)
                    {
                        GameObject projectile = Instantiate(baseProjectile, transform.position, rotation);
                        projectileScript current = projectile.GetComponent<projectileScript>();
                        current.parentTower = this;
                        current.target = currentTarget.transform.position;
                        projectile.GetComponent<SpriteRenderer>().sprite = baseCard.projectileImage;
                    }
                    hasAttacked = true;
                }
                yield return null;
            }

            yield return new WaitForSeconds(secondsPerAttack);

            hasAttacked = false;
        }
    }
}
