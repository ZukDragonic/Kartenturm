using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectParent", menuName = "Card/Effect/Parent")]
public class SOCE_Parent : ScriptableObject
{
    public SOC_Effect[] passive;
    public SOC_Effect[] destroy;
    public SOC_Effect[] summon;
    public SOC_Effect[] waveStart;
    public SOC_Effect[] waveEnd;
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
    public void OnWaveStartEffect(baseTowerScript script)
    {
        playEffectFromArray(waveStart, script, null);
    }
    public void OnWaveEndEffect(baseTowerScript script)
    {
        playEffectFromArray(waveEnd, script, null);
    }
    public void OPassiveEffect(sumObj_Base script)
    {
        playEffectFromArray(pPassive, null, script);
    }
    public void OnODestroyEffect(sumObj_Base script)
    {
        playEffectFromArray(pDestroy, null, script);
    }
    public void OnOHitEffect(sumObj_Base script)
    {
        playEffectFromArray(pHit, null, script);
    }
    public void OnOSummonEffect(sumObj_Base script)
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

    public static SOCE_Parent CreateInstance(SOCE_Parent copied)
    {
        SOCE_Parent current = Instantiate(copied);

        List<SOC_Effect> newEffects = new List<SOC_Effect>();
        foreach (SOC_Effect effect in copied.passive)
        {
            newEffects.Add(Instantiate(effect));
        }
        current.passive = newEffects.ToArray();

        newEffects = new List<SOC_Effect>();
        foreach (SOC_Effect effect in copied.destroy)
        {
            newEffects.Add(Instantiate(effect));
        }
        current.destroy = newEffects.ToArray();
        
        newEffects = new List<SOC_Effect>();
        foreach (SOC_Effect effect in copied.summon)
        {
            newEffects.Add(Instantiate(effect));
        }
        current.summon = newEffects.ToArray();
        
        newEffects = new List<SOC_Effect>();
        foreach (SOC_Effect effect in copied.waveStart)
        {
            newEffects.Add(Instantiate(effect));
        }
        current.waveStart = newEffects.ToArray();
        
        newEffects = new List<SOC_Effect>();
        foreach (SOC_Effect effect in copied.waveEnd)
        {
            newEffects.Add(Instantiate(effect));
        }
        current.waveEnd = newEffects.ToArray();
        
        newEffects = new List<SOC_Effect>();
        foreach (SOC_Effect effect in copied.pPassive)
        {
            newEffects.Add(Instantiate(effect));
        }
        current.pPassive = newEffects.ToArray();
        
        newEffects = new List<SOC_Effect>();
        foreach (SOC_Effect effect in copied.pHit)
        {
            newEffects.Add(Instantiate(effect));
        }
        current.pHit = newEffects.ToArray();
        
        newEffects = new List<SOC_Effect>();
        foreach (SOC_Effect effect in copied.pSummon)
        {
            newEffects.Add(Instantiate(effect));
        }
        current.pSummon = newEffects.ToArray();
        return current;
    }
}