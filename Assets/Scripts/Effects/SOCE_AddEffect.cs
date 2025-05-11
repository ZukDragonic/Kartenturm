using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectAddeffect", menuName = "Card/Effect/Add Effect")]
public class SOCE_AddEffect : SOC_Effect
{
    public EffectType effectType;
    public SOC_Effect addedEffect;

    public bool appliedToHit;

    public override void ApplyEEffect(SOEnemyDisplay script)
    {
        sumObj_Base checkedScript;
        if (appliedToHit)
        {
            checkedScript = script.currentAffectedTower;
        }
        else
        {
            checkedScript = script;
        }

        switch (effectType)
        {
            case EffectType.Passive:
                checkedScript.effects.passive = AddItemToArray(checkedScript.effects.passive, addedEffect);
                break;
            case EffectType.Destroy:
                checkedScript.effects.destroy = AddItemToArray(checkedScript.effects.destroy, addedEffect);
                break;
            case EffectType.Summon:
                checkedScript.effects.summon = AddItemToArray(checkedScript.effects.summon, addedEffect);
                break;
            case EffectType.Hit:
                checkedScript.effects.hit = AddItemToArray(checkedScript.effects.hit, addedEffect);
                break;
            case EffectType.WaveStart:
                checkedScript.effects.waveStart = AddItemToArray(checkedScript.effects.waveStart, addedEffect);
                break;
            case EffectType.WaveEnd:
                checkedScript.effects.waveEnd = AddItemToArray(checkedScript.effects.waveEnd, addedEffect);
                break;
            case EffectType.PPassive:
                checkedScript.effects.pPassive = AddItemToArray(checkedScript.effects.pPassive, addedEffect);
                break;
            case EffectType.PDestroy:
                checkedScript.effects.pDestroy = AddItemToArray(checkedScript.effects.pDestroy, addedEffect);
                break;
            case EffectType.PHit:
                checkedScript.effects.pHit = AddItemToArray(checkedScript.effects.pHit, addedEffect);
                break;
            case EffectType.PSummon:
                checkedScript.effects.pSummon = AddItemToArray(checkedScript.effects.pSummon, addedEffect);
                break;
            default:
                break;
        }
    }
    public override void ApplyTEffect(baseTowerScript script)
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
            case EffectType.Hit:
                script.effects.hit = AddItemToArray(script.effects.hit, addedEffect);
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
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        sumObj_Base checkedScript;
        if (appliedToHit)
        {
            checkedScript = script.hitEnemy;
        }
        else
        {
            checkedScript = script;
        }

        switch (effectType)
        {
            case EffectType.Passive:
                checkedScript.effects.passive = AddItemToArray(checkedScript.effects.passive, addedEffect);
                break;
            case EffectType.Destroy:
                checkedScript.effects.destroy = AddItemToArray(checkedScript.effects.destroy, addedEffect);
                break;
            case EffectType.Summon:
                checkedScript.effects.summon = AddItemToArray(checkedScript.effects.summon, addedEffect);
                break;
            case EffectType.Hit:
                checkedScript.effects.hit = AddItemToArray(checkedScript.effects.hit, addedEffect);
                break;
            case EffectType.WaveStart:
                checkedScript.effects.waveStart = AddItemToArray(checkedScript.effects.waveStart, addedEffect);
                break;
            case EffectType.WaveEnd:
                checkedScript.effects.waveEnd = AddItemToArray(checkedScript.effects.waveEnd, addedEffect);
                break;
            case EffectType.PPassive:
                checkedScript.effects.pPassive = AddItemToArray(checkedScript.effects.pPassive, addedEffect);
                break;
            case EffectType.PDestroy:
                checkedScript.effects.pDestroy = AddItemToArray(checkedScript.effects.pDestroy, addedEffect);
                break;
            case EffectType.PHit:
                checkedScript.effects.pHit = AddItemToArray(checkedScript.effects.pHit, addedEffect);
                break;
            case EffectType.PSummon:
                checkedScript.effects.pSummon = AddItemToArray(checkedScript.effects.pSummon, addedEffect);
                break;
            default:
                break;
        }
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        sumObj_Base checkedScript;
        if (appliedToHit)
        {
            checkedScript = script.hitEnemy;
        }
        else
        {
            checkedScript = script;
        }

        switch (effectType)
        {
            case EffectType.Passive:
                checkedScript.effects.passive = AddItemToArray(checkedScript.effects.passive, addedEffect);
                break;
            case EffectType.Destroy:
                checkedScript.effects.destroy = AddItemToArray(checkedScript.effects.destroy, addedEffect);
                break;
            case EffectType.Summon:
                checkedScript.effects.summon = AddItemToArray(checkedScript.effects.summon, addedEffect);
                break;
            case EffectType.Hit:
                checkedScript.effects.hit = AddItemToArray(checkedScript.effects.hit, addedEffect);
                break;
            case EffectType.WaveStart:
                checkedScript.effects.waveStart = AddItemToArray(checkedScript.effects.waveStart, addedEffect);
                break;
            case EffectType.WaveEnd:
                checkedScript.effects.waveEnd = AddItemToArray(checkedScript.effects.waveEnd, addedEffect);
                break;
            case EffectType.PPassive:
                checkedScript.effects.pPassive = AddItemToArray(checkedScript.effects.pPassive, addedEffect);
                break;
            case EffectType.PDestroy:
                checkedScript.effects.pDestroy = AddItemToArray(checkedScript.effects.pDestroy, addedEffect);
                break;
            case EffectType.PHit:
                checkedScript.effects.pHit = AddItemToArray(checkedScript.effects.pHit, addedEffect);
                break;
            case EffectType.PSummon:
                checkedScript.effects.pSummon = AddItemToArray(checkedScript.effects.pSummon, addedEffect);
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

