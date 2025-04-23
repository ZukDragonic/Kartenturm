using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectAddeffect", menuName = "Card/Effect/Add Effect")]
public class SOCE_AddEffect : SOC_Effect
{
    public EffectType effectType;
    public SOC_Effect addedEffect;
    //req starts as false, returns true if even 1 instance of the effect is found
    public override void ApplyTEffect(baseTowerScript script)
    {
        switch (effectType)
        {
            case EffectType.Passive:
                script.currentEffect.passive = AddItemToArray(script.currentEffect.passive, addedEffect);
                break;
            case EffectType.Destroy:
                script.currentEffect.destroy = AddItemToArray(script.currentEffect.destroy, addedEffect);
                break;
            case EffectType.Summon:
                script.currentEffect.summon = AddItemToArray(script.currentEffect.summon, addedEffect);
                break;
            case EffectType.WaveStart:
                script.currentEffect.waveStart = AddItemToArray(script.currentEffect.waveStart, addedEffect);
                break;
            case EffectType.WaveEnd:
                script.currentEffect.waveEnd = AddItemToArray(script.currentEffect.waveEnd, addedEffect);
                break;
            case EffectType.PPassive:
                script.currentEffect.pPassive = AddItemToArray(script.currentEffect.pPassive, addedEffect);
                break;
            case EffectType.PDestroy:
                script.currentEffect.pDestroy = AddItemToArray(script.currentEffect.pDestroy, addedEffect);
                break;
            case EffectType.PHit:
                script.currentEffect.pHit = AddItemToArray(script.currentEffect.pHit, addedEffect);
                break;
            case EffectType.PSummon:
                script.currentEffect.pSummon = AddItemToArray(script.currentEffect.pSummon, addedEffect);
                break;
            default:
                break;
        }
    }
    public override void ApplyOSEffect(sumObj_Base script)
    {
        switch (effectType)
        {
            case EffectType.Passive:
                script.effects.passive = AddItemToArray(script.effects.passive, addedEffect);
                break;
            case EffectType.Destroy:
                script.effects.destroy = AddItemToArray(script.effects.destroy, addedEffect);
                break;
            case EffectType.Summon:
                script.effects.summon = AddItemToArray(script.effects.summon, addedEffect);
                break;
            case EffectType.WaveStart:
                script.effects.waveStart = AddItemToArray(script.effects.waveStart, addedEffect);
                break;
            case EffectType.WaveEnd:
                script.effects.waveEnd = AddItemToArray(script.effects.waveEnd, addedEffect);
                break;
            case EffectType.PPassive:
                script.effects.pPassive = AddItemToArray(script.effects.pPassive, addedEffect);
                break;
            case EffectType.PDestroy:
                script.effects.pDestroy = AddItemToArray(script.effects.pDestroy, addedEffect);
                break;
            case EffectType.PHit:
                script.effects.pHit = AddItemToArray(script.effects.pHit, addedEffect);
                break;
            case EffectType.PSummon:
                script.effects.pSummon = AddItemToArray(script.effects.pSummon, addedEffect);
                break;
            default:
                break;
        }
    }
    public override void ApplyOPEffect(sumObj_Base script)
    {
        switch (effectType)
        {
            case EffectType.Passive:
                script.effects.passive = AddItemToArray(script.effects.passive, addedEffect);
                break;
            case EffectType.Destroy:
                script.effects.destroy = AddItemToArray(script.effects.destroy, addedEffect);
                break;
            case EffectType.Summon:
                script.effects.summon = AddItemToArray(script.effects.summon, addedEffect);
                break;
            case EffectType.WaveStart:
                script.effects.waveStart = AddItemToArray(script.effects.waveStart, addedEffect);
                break;
            case EffectType.WaveEnd:
                script.effects.waveEnd = AddItemToArray(script.effects.waveEnd, addedEffect);
                break;
            case EffectType.PPassive:
                script.effects.pPassive = AddItemToArray(script.effects.pPassive, addedEffect);
                break;
            case EffectType.PDestroy:
                script.effects.pDestroy = AddItemToArray(script.effects.pDestroy, addedEffect);
                break;
            case EffectType.PHit:
                script.effects.pHit = AddItemToArray(script.effects.pHit, addedEffect);
                break;
            case EffectType.PSummon:
                script.effects.pSummon = AddItemToArray(script.effects.pSummon, addedEffect);
                break;
            default:
                break;
        }
    }
    public override void ApplyBEffect()
    {
        Debug.Log("Does Not Exist");
    }

    EffectAndReq[] AddItemToArray(EffectAndReq[] CheckedEnR, SOC_Effect AddedEffect)
    {
        //created because i don't wanna type the same shit 10 times
        List<EffectAndReq> returnedEffect = CheckedEnR.ToList<EffectAndReq>();
        EffectAndReq newEnR = new EffectAndReq();
        newEnR.effect = Instantiate(addedEffect);
        newEnR.req = new SOC_Requirement[] { };
        returnedEffect.Add(newEnR);
        return returnedEffect.ToArray();
    }
}

