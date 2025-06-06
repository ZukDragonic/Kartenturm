using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectSummon", menuName = "Card/Effect/Summon")]
public class SOCE_Summon : SOC_Effect
{
    public GameObject summonedPrefab;

    //specifically for standalone or projectile
    public SOSumObj summonedObject;
    //specifically for tower
    public SOCTower summonedTower;
    //specifically for enemy
    public SOEnemy summonedEnemy;

    public bool keepSlot;
    public override void ApplyEEffect(SOEnemyDisplay script)
    {
        GameObject current = Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity, script.transform.parent);
        SOEnemyDisplay summonedScript = current.GetComponent<SOEnemyDisplay>();
        summonedScript.enemy = summonedEnemy;
        gameControllerScript.instance.resourceManager.addEnemy(summonedScript);
    }
    public override void ApplyTEffect(baseTowerScript script)
    {
        GameObject current = Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity, script.transform.parent);
        baseTowerScript summonedScript = current.GetComponent<baseTowerScript>();
        if (keepSlot)
        {
            summonedScript.stackedCards = script.stackedCards;
        }
        summonedScript.baseCard = summonedTower;
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        GameObject current = Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity);
        sumObj_Standalone summonedScript = current.GetComponent<sumObj_Standalone>();
        summonedScript.baseSumObj = summonedObject;
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        GameObject current = Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity);
        sumObj_Standalone summonedScript = current.GetComponent<sumObj_Standalone>();
        summonedScript.baseSumObj = summonedObject;
        summonedScript.parentProjectile = script;
    }
    public override void ApplyBEffect()
    {
        GameObject summoned = Instantiate(summonedPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        summoned.GetComponent<sumObj_Standalone>().baseSumObj = summonedObject as SOSumObj;
    }
}
