using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectRemoveeffect", menuName = "Card/Effect/Remove Effect")]
public class SOCE_RemoveEffect : SOC_Effect
{
    public SOC_Effect checkedEffect;
    //req starts as false, returns true if even 1 instance of the effect is found
    public override void ApplyTEffect(baseTowerScript script)
    {
        foreach (EffectAndReq EnR in script.effects.passive)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.passive =  RemoveItemFromArray(script.effects.passive, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.destroy)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.destroy =  RemoveItemFromArray(script.effects.destroy, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.summon)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.summon =  RemoveItemFromArray(script.effects.summon, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.hit)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.hit = RemoveItemFromArray(script.effects.hit, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.waveStart)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.waveStart =  RemoveItemFromArray(script.effects.waveStart, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.waveEnd)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.waveEnd =  RemoveItemFromArray(script.effects.waveEnd, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pPassive)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pPassive = RemoveItemFromArray(script.effects.pPassive, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pDestroy)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pDestroy = RemoveItemFromArray(script.effects.pDestroy, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pHit)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pHit =  RemoveItemFromArray(script.effects.pHit, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pSummon)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pSummon =  RemoveItemFromArray(script.effects.pSummon, EnR);
            }
        }
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        foreach (EffectAndReq EnR in script.effects.passive)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.passive =  RemoveItemFromArray(script.effects.passive, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.destroy)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.destroy =  RemoveItemFromArray(script.effects.destroy, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.summon)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.summon =  RemoveItemFromArray(script.effects.summon, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.hit)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.hit = RemoveItemFromArray(script.effects.hit, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.waveStart)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.waveStart =  RemoveItemFromArray(script.effects.waveStart, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.waveEnd)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.waveEnd =  RemoveItemFromArray(script.effects.waveEnd, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pPassive)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pPassive =  RemoveItemFromArray(script.effects.pPassive, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pDestroy)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pDestroy = RemoveItemFromArray(script.effects.pDestroy, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pHit)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pHit =  RemoveItemFromArray(script.effects.pHit, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pSummon)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pSummon =  RemoveItemFromArray(script.effects.pSummon, EnR);
            }
        }
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        foreach (EffectAndReq EnR in script.effects.passive)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.passive = RemoveItemFromArray(script.effects.passive, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.destroy)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.destroy = RemoveItemFromArray(script.effects.destroy, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.summon)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.summon = RemoveItemFromArray(script.effects.summon, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.hit)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.hit = RemoveItemFromArray(script.effects.hit, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.waveStart)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.waveStart = RemoveItemFromArray(script.effects.waveStart, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.waveEnd)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.waveEnd = RemoveItemFromArray(script.effects.waveEnd, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pPassive)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pPassive = RemoveItemFromArray(script.effects.pPassive, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pDestroy)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pDestroy = RemoveItemFromArray(script.effects.pDestroy, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pHit)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pHit = RemoveItemFromArray(script.effects.pHit, EnR);
            }
        }
        foreach (EffectAndReq EnR in script.effects.pSummon)
        {
            if (CheckSameEffect(EnR.effect))
            {
                script.effects.pSummon = RemoveItemFromArray(script.effects.pSummon, EnR);
            }
        }
    }
    public override void ApplyBEffect()
    {
        Debug.Log("Does Not Exist");
    }

    bool CheckSameEffect(SOC_Effect comparedEffect)
    {
        //created because i don't wanna type the same shit 10 times
        if (comparedEffect.name.Replace("(Clone)", "") == checkedEffect.name)
        {
            return true;
        }
        return false;
    }

    EffectAndReq[] RemoveItemFromArray(EffectAndReq[] CheckedEnR,EffectAndReq RemovedEnR)
    {
        //created because i don't wanna type the same shit 10 times
        List<EffectAndReq> returnedEffect = CheckedEnR.ToList<EffectAndReq>();
        returnedEffect.Remove(RemovedEnR);
        return returnedEffect.ToArray();
    }
}
