using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTowerEffectSummon", menuName = "Card/TowerEffect/Summon")]
public class SOCTE_Summon : SOCTEffect
{
    public GameObject summonedPrefab;
    public override void ApplyTEffect(baseTowerScript script)
    {
        Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity);
    }
    public override void ApplyPEffect(projectileScript script)
    {
        Instantiate(summonedPrefab, script.gameObject.transform.position, Quaternion.identity);
    }
}
