using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectSummon", menuName = "Card/Effect/Summon")]
public class SOCE_Summon : SOC_Effect
{
    public SOCE_Parent summonedEffects;
    public GameObject summonedPrefab;

    public SOCTower summonedTower;
    public override void ApplyTEffect(baseTowerScript script)
    {
        GameObject current = Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity, script.transform.parent);
        baseTowerScript summonedScript = current.GetComponent<baseTowerScript>();
        summonedScript.stackedCards = script.stackedCards;
        summonedScript.baseCard = summonedTower;
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        GameObject current = Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity);
        sumObj_Standalone summonedScript = current.GetComponent<sumObj_Standalone>();
        summonedScript.effects = summonedEffects;
        summonedScript.parentProjectile = script as sumObj_Projectile;
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        GameObject current = Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity);
        sumObj_Standalone summonedScript = current.GetComponent<sumObj_Standalone>();
        summonedScript.effects = summonedEffects;
        summonedScript.parentProjectile = script as sumObj_Projectile;
    }
    public override void ApplyBEffect()
    {
        Instantiate(summonedPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }
}
