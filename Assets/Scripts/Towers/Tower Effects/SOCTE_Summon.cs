using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTowerEffectSummon", menuName = "Card/TowerEffect/Summon")]
public class SOCTE_Summon : SOCTEffect
{
    public GameObject summonedPrefab;
    public override void ApplyTEffect(baseTowerScript script)
    {
        GameObject current = Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity);
        current.GetComponent<SumObj_Base>().parentTower = script;
    }
    public override void ApplyPEffect(projectileScript script)
    {
        GameObject current = Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity);
        current.GetComponent<SumObj_Base>().parentTower = script.parentTower;
    }
}
