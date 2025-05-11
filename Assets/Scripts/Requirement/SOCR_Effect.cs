using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardRequirementEffect", menuName = "Card/Requirement/Effect")]
public class SOCR_Effect : SOC_Requirement
{
    public SOC_Effect checkedEffect;
    //req starts as false, returns true if even 1 instance of the effect is found
    public override bool ApplyTRequirement(baseTowerScript script)
    {
        requirement = false;
        
        foreach (EffectAndReq EnR in script.effects.passive)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.destroy)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.summon)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.hit)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.waveStart)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.waveEnd)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pPassive)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pDestroy)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pHit)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pSummon)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyOSRequirement(sumObj_Standalone script)
    {
        requirement = false;

        foreach (EffectAndReq EnR in script.effects.passive)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.destroy)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.summon)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.hit)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.waveStart)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.waveEnd)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pPassive)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pDestroy)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pHit)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pSummon)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }

        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyOPRequirement(sumObj_Projectile script)
    {
        requirement = false;

        foreach (EffectAndReq EnR in script.effects.passive)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.destroy)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.summon)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.hit)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.waveStart)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.waveEnd)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pPassive)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pDestroy)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pHit)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }
        foreach (EffectAndReq EnR in script.effects.pSummon)
        {
            FalseReqIfEffectNotSame(EnR.effect);
        }

        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyBRequirement()
    {
        Debug.Log("DNE");
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }

    void FalseReqIfEffectNotSame(SOC_Effect comparedEffect)
    {
        //created because i don't wanna type the same shit 10 times
        if (comparedEffect.name.Replace("(Clone)", "") == checkedEffect.name)
        {
            requirement = true;
        }
    }
}
