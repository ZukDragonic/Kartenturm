using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectParent", menuName = "Card/Effect/Parent")]
public class SOCE_Parent : ScriptableObject
{
    public EffectAndReq[] passive;
    public EffectAndReq[] destroy;
    public EffectAndReq[] summon;
    public EffectAndReq[] waveStart;
    public EffectAndReq[] waveEnd;
    public EffectAndReq[] pPassive;
    public EffectAndReq[] pDestroy;
    public EffectAndReq[] pHit;
    public EffectAndReq[] pSummon;


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

    void playEffectFromArray(EffectAndReq[] applied, baseTowerScript tScript, sumObj_Base pScript)
    {
        if (applied == null)
        {
            return;
        }

        foreach (EffectAndReq effectAndReq in applied)
        {
            if (effectAndReq.CheckRequirement(tScript, pScript))
            {
                effectAndReq.effect.ApplyEffect(tScript, pScript);
            }
        }
    }

    public static EffectAndReq instantiateEnR(EffectAndReq oldEnR)
    {
        EffectAndReq currentEnR = new EffectAndReq();
        currentEnR.effect = Instantiate(oldEnR.effect);
        List<SOC_Requirement> reqList = new List<SOC_Requirement>();
        foreach (SOC_Requirement req in oldEnR.req)
        {
            reqList.Add(Instantiate(req));
        }
        currentEnR.req = reqList.ToArray();
        return currentEnR;
    }
    public static SOCE_Parent CreateInstance(SOCE_Parent copied)
    {
        SOCE_Parent current = ScriptableObject.CreateInstance<SOCE_Parent>();

        List<EffectAndReq> newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in copied.passive)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.passive = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in copied.destroy)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.destroy = newEnR.ToArray();
        
        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in copied.summon)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.summon = newEnR.ToArray();
        
        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in copied.waveStart)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.waveStart = newEnR.ToArray();
        
        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in copied.waveEnd)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.waveEnd = newEnR.ToArray();
        
        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in copied.pPassive)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.pPassive = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in copied.pDestroy)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.pDestroy = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in copied.pHit)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.pHit = newEnR.ToArray();
        
        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in copied.pSummon)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.pSummon = newEnR.ToArray();
        return current;
    }
    public static SOCE_Parent MergeInstances(SOCE_Parent mergerOne, SOCE_Parent mergerTwo)
    {
        SOCE_Parent current = ScriptableObject.CreateInstance<SOCE_Parent>();

        List<EffectAndReq> newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in mergerOne.passive)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        foreach (EffectAndReq EnR in mergerTwo.passive)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.passive = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in mergerOne.destroy)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        foreach (EffectAndReq EnR in mergerTwo.destroy)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.destroy = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in mergerOne.summon)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        foreach (EffectAndReq EnR in mergerTwo.summon)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.summon = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in mergerOne.waveStart)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        foreach (EffectAndReq EnR in mergerTwo.waveStart)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.waveStart = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in mergerOne.waveEnd)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        foreach (EffectAndReq EnR in mergerTwo.waveEnd)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.waveEnd = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in mergerOne.pPassive)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        foreach (EffectAndReq EnR in mergerTwo.pPassive)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.pPassive = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in mergerOne.pDestroy)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        foreach (EffectAndReq EnR in mergerTwo.pDestroy)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.pDestroy = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in mergerOne.pHit)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        foreach (EffectAndReq EnR in mergerTwo.pHit)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.pHit = newEnR.ToArray();

        newEnR = new List<EffectAndReq>();
        foreach (EffectAndReq EnR in mergerOne.pSummon)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        foreach (EffectAndReq EnR in mergerTwo.pSummon)
        {
            newEnR.Add(instantiateEnR(EnR));
        }
        current.pSummon = newEnR.ToArray();
        return current;
    }
}