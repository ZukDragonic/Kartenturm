using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTowerEffectDestroy", menuName = "Card/TowerEffect/Destroy")]
public class SOCTE_Destroy : SOCTEffect
{
    public float duration;
    public override void ApplyTEffect(baseTowerScript script)
    {
        Destroy(script.gameObject, duration);
    }
    public override void ApplyPEffect(projectileScript script)
    {
        //makes duration scale in respect to range
        duration *= (script.parentTower.range / script.parentTower.baseCard.range);
        Destroy(script.gameObject, duration);
    }
}
