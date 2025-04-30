using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
public class baseTowerScript : sumObj_Base
{
    public SOCTower baseCard;
    public List<SOCard> stackedCards;
    public SOCTower placeHolderCard;

    public SOCE_Parent currentEffect;

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
    public targettingBase[] allTargetting;


    public int targetChange;

    public float range;
    public int damage;
    public int pierce;
    
    public int shotsPerAttack;
    public float secondsPerAttack;
    public float multiplier;

    public GameObject baseProjectile;
   
    void Start()
    {
        cosmetic = GetComponentInChildren<tCosmeticScript>();
        cosmetic.showTower(baseCard);

        allTargetting = baseCard.allTargetting;
        currentTargetting = allTargetting[0];

        range = baseCard.range;
        damage = baseCard.damage;
        pierce = baseCard.pierce;
        shotsPerAttack = baseCard.shotsPerAttack;
        secondsPerAttack = baseCard.secondsPerAttack;
        currentEffect = SOCE_Parent.CreateInstance(baseCard.effects);
        transform.GetChild(0).GetComponent<CircleCollider2D>().radius = range / 10 / transform.localScale.x / 3.125f;
        multiplier = baseCard.multiplier;

        hasAttacked = false;

        enemyInRange = new List<GameObject>();

        stackedCards.Add(placeHolderCard);
        stackedCards.Add(placeHolderCard);
        stackedCards.Add(placeHolderCard);

        gameController = gameControllerScript.instance;
        gameController.resourceManager.addTower(this);

        changeStats();
        cosmetic.rangeDisplayUpdate(false, range);

        StartCoroutine(attackCycle());
    }
    void Update()
    {
        if (gameController.state == GameState.PauseMenu || gameController.state == GameState.ShopMenu)
        {
            return;
        }
        currentEffect.PassiveEffect(this);
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
        StartCoroutine(delayedOpenTowerMenu());
    }
    IEnumerator delayedOpenTowerMenu()
    {
        yield return null;
        openedTowerMenu = null;
        gameController.UpdateGameState(GameState.TowerMenu);

        if (Camera.main.WorldToViewportPoint(gameObject.transform.position).x < 0.5f)
        {
            openedTowerMenu = Instantiate(towerMenuPrefabR, gameController.allCanvas[1].transform);
        }
        else
        {
            openedTowerMenu = Instantiate(towerMenuPrefabL, gameController.allCanvas[1].transform);
        }
        openedTowerMenu.GetComponent<towerMenuDisplay>().parentTower = this;
        openedTowerMenu.GetComponentInChildren<slotParentScriptUGUI>().parentTower = this;
        openedTowerMenu.GetComponentInChildren<destroyTowerScript>().parentTower = this;

        cosmetic.rangeDisplayUpdate(true, range);
    }
    public void closeTowerMenu()
    {
        isSelected = false;
        gameController.UpdateGameState(GameState.Previous);
        if (openedTowerMenu != null)
        {
            Destroy(openedTowerMenu);
        }
        openedTowerMenu = null;
        cosmetic.rangeDisplayUpdate(false, range);
    }

    public void changeStats()
    {
        currentEffect = SOCE_Parent.CreateInstance(baseCard.effects);
        damage = baseCard.damage;
        pierce = baseCard.pierce;
        range = baseCard.range;
        multiplier = baseCard.multiplier;
        secondsPerAttack = baseCard.secondsPerAttack;
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            float ogScale = transform.GetChild(i).localScale.x;
            transform.GetChild(i).localScale = Vector3.one;
        }

        foreach (SOCard slot in stackedCards)
        {
            if (slot as SOCArtifact != null)
            {
                SOCArtifact artifactSlot = slot as SOCArtifact;
                currentEffect = SOCE_Parent.MergeInstances(currentEffect, artifactSlot.addedEffects);
            }
        }
        currentEffect.OnSummonEffect(this);
        foreach (SOCard slot in stackedCards)
        {
            if (slot as SOCTower != null)
            {
                SOCTower towerSlot = slot as SOCTower;
                damage += (int)(towerSlot.damage / 2 * multiplier);
                pierce += (int)(towerSlot.pierce / 2 * multiplier);
                range += towerSlot.range / 2 * multiplier;
            }
        }
        cosmetic.rangeDisplayUpdate(true, range);
        transform.GetChild(0).GetComponent<CircleCollider2D>().radius = range / 10 / transform.localScale.x / 3.125f;
    }

    public void changeTargetting(int change)
    {
        targetChange = change;
        currentTargetting.ExitCheck(this);
    }

    IEnumerator attackCycle()
    {
        bool isTargetNone = baseCard.targetting == targettingOption.None;
        while (!isTargetNone)
        {
            while (!hasAttacked)
            {
                //check for target
                currentTargetting.UpdateCheck(this);

                if (currentTarget != null)
                {
                    Vector3 target = Vector3.zero;
                    //face target
                    target = currentTarget.transform.position;

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
                        GameObject projectile = Instantiate(baseProjectile, transform.position, Quaternion.identity);
                        sumObj_Projectile current = projectile.GetComponent<sumObj_Projectile>();
                        current.parentTower = this;
                        current.target = currentTarget.transform.position;
                        projectile.GetComponent<SpriteRenderer>().sprite = baseCard.projectileImage;
                    }
                    currentEffect.OnHitEffect(this);
                    hasAttacked = true;
                }
                yield return null;
            }
            float attackTimer = 0;

            while (attackTimer < secondsPerAttack)
            {
                attackTimer += 1f / (float)Application.targetFrameRate * (float)gameController.timeManager.currentTime;
                yield return null;
            }
            hasAttacked = false;
        }
    }
    void OnDestroy()
    {
        currentEffect.OnDestroyEffect(this);
    }
    public void waveStartAction()
    {
        currentEffect.OnWaveStartEffect(this);
    }
    public void waveEndAction()
    {
        currentEffect.OnWaveEndEffect(this);
    }
}
