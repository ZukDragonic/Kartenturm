using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTowerEffectParent", menuName = "Card/TowerEffectParent")]
public class SOCTE_Parent : ScriptableObject
{
    public SOCTEffect[] passive;
    public SOCTEffect[] destroy;
    public SOCTEffect[] summon;
    public SOCTEffect[] pPassive;
    public SOCTEffect[] pDestroy;
    public SOCTEffect[] pHit;
    public SOCTEffect[] pSummon;
    
    
    public void PassiveEffect(baseTowerScript script)
    {
        playEffectFromArray(passive, script, null);
    }
    public void OnDestroyEffect(baseTowerScript script)
    {
        playEffectFromArray(destroy, script, null);
    }
    public void OnSummonEffect(baseTowerScript script)
    {
        playEffectFromArray(summon, script, null);
    }

    public void PPassiveEffect(projectileScript script)
    {
        playEffectFromArray(pPassive, null, script);
    }
    public void OnPDestroyEffect(projectileScript script)
    {
        playEffectFromArray(pDestroy, null, script);
    }
    public void OnPHitEffect(projectileScript script)
    {
        playEffectFromArray(pHit, null, script);
    }
    public void OnPSummonEffect(projectileScript script)
    {
        playEffectFromArray(pSummon, null, script);
    }

    void playEffectFromArray(SOCTEffect[] allEffect, baseTowerScript tScript, projectileScript pScript)
    {
        foreach (SOCTEffect effect in allEffect)
        {
            effect.ApplyEffect(tScript, pScript);
        }
    }
}