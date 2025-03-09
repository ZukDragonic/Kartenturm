using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectParent", menuName = "Card/Effect/Parent")]
public class SOCE_Parent : ScriptableObject
{
    public SOC_Effect[] passive;
    public SOC_Effect[] destroy;
    public SOC_Effect[] summon;
    public SOC_Effect[] pPassive;
    public SOC_Effect[] pDestroy;
    public SOC_Effect[] pHit;
    public SOC_Effect[] pSummon;
    
    
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

    public void PPassiveEffect(sumObj_Base script)
    {
        playEffectFromArray(pPassive, null, script);
    }
    public void OnPDestroyEffect(sumObj_Base script)
    {
        playEffectFromArray(pDestroy, null, script);
    }
    public void OnPHitEffect(sumObj_Base script)
    {
        playEffectFromArray(pHit, null, script);
    }
    public void OnPSummonEffect(sumObj_Base script)
    {
        playEffectFromArray(pSummon, null, script);
    }

    void playEffectFromArray(SOC_Effect[] allEffect, baseTowerScript tScript, sumObj_Base pScript)
    {
        foreach (SOC_Effect effect in allEffect)
        {
            effect.ApplyEffect(tScript, pScript);
        }
    }
}