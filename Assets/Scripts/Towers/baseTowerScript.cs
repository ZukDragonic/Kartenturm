using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
public class baseTowerScript : MonoBehaviour
{
    public SOCTower baseCard;
    public List<SOCTower> stackedCards;
    public SOCTower placeHolderCard;

    private bool hasAttacked;

    public Material hoverMat;
    public Material clickMat;
    public GameObject rangeIndicatorPrefab;
    private GameObject rIObject;
    private Material defaultMat;
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
    public float projectileDuration;
    
    void Start()
    {
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


        GetComponent<SpriteRenderer>().sprite = baseCard.image;
        defaultMat = GetComponent<SpriteRenderer>().material;


        range = baseCard.range;
        attackValue = baseCard.attackValue;
        projectilePierce = baseCard.projectilePierce;
        shotsPerAttack = baseCard.shotsPerAttack;
        secondsPerAttack = baseCard.secondsPerAttack;
        projectileDuration = baseCard.projectileDuration;
        transform.GetChild(0).GetComponent<CircleCollider2D>().radius = range / 10 / transform.localScale.x;

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
        Debug.Log(currentTargetting.TargettingCheck());
        //hover highlight, click hightlight, and gui open/close
        //if mouse is over the tower
        if (gameController.mainInput_Ray.hit == gameObject)
        {
            if (!isSelected)
            {
                if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.material = hoverMat;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (openedTowerMenu != null)
                        {
                            closeTowerMenu();
                        }
                        spriteRenderer.material = clickMat;

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
                        spriteRenderer.material = defaultMat;
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
                    spriteRenderer.material = defaultMat;
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
        gameController.UpdateGameState(gameControllerScript.GameState.GameMenu);

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

        rIObject = Instantiate(rangeIndicatorPrefab, transform.position, Quaternion.identity, openedTowerMenu.transform);
        rIObject.GetComponent<Transform>().localScale = Vector3.one * range / 5;
    }
    public void closeTowerMenu()
    {
        gameController.UpdateGameState(gameControllerScript.GameState.Wave);
        Destroy(openedTowerMenu);
        openedTowerMenu = null;
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
        transform.GetChild(0).GetComponent<CircleCollider2D>().radius = range / 10 / transform.localScale.x;
        rIObject.GetComponent<Transform>().localScale = Vector3.one * range / 5;

        //makes duration scale in respect to range
        projectileDuration = baseCard.projectileDuration * (range / baseCard.range);
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
                    //points at target
                    Vector3 target = Vector3.zero;
                    if (targetEnemy || targetAlly)
                    {
                        target = currentTarget.transform.position - transform.position;
                    }
                    else if (targetNone)
                    {
                        int ran = Random.Range(0, 360);
                        float ranRadian = ran / 180 * 3.14f;
                        float xPos = Mathf.Cos(ranRadian);
                        float yPos = Mathf.Sin(ranRadian);
                        target = new Vector3(xPos, yPos, 0);
                    }

                    float rotZ = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, rotZ);

                    //spawns attack
                    for (int i = 0; i < shotsPerAttack; i++)
                    {
                        GameObject projectile = Instantiate(baseCard.projectileObject, transform.position, transform.rotation);
                        projectile.GetComponent<projectileScript>().parentTower = this;
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
