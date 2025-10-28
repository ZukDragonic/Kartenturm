using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectTimeadd", menuName = "Card/Effect/TimedAdd")]
public class SOCE_TimedAdd : SOC_Effect
{
    public float duration;
    public EffectType effectType;
    public SOC_Effect addedEffect;
    private EffectAndReq[] addedLocation;
    private EffectAndReq referenceEnR;

    private int frameCount = 0;
    private float relativeDuration = 0;

    private float OGValue = 0;

    public override void ApplyEEffect(SOEnemyDisplay script)
    {
        if (OGValue == 0)
        {
            sumObj_Base checkedScript = script;
            switch (effectType)
            {
                case EffectType.Passive:
                    addedLocation = checkedScript.effects.passive;
                    checkedScript.effects.passive = AddItemToArray(checkedScript.effects.passive, addedEffect);
                    break;
                case EffectType.Destroy:
                    addedLocation = checkedScript.effects.destroy;
                    checkedScript.effects.destroy = AddItemToArray(checkedScript.effects.destroy, addedEffect);
                    break;
                case EffectType.Summon:
                    addedLocation = checkedScript.effects.summon;
                    checkedScript.effects.summon = AddItemToArray(checkedScript.effects.summon, addedEffect);
                    break;
                case EffectType.Hit:
                    addedLocation = checkedScript.effects.hit;
                    checkedScript.effects.hit = AddItemToArray(checkedScript.effects.hit, addedEffect);
                    break;
                case EffectType.WaveStart:
                    addedLocation = checkedScript.effects.waveStart;
                    checkedScript.effects.waveStart = AddItemToArray(checkedScript.effects.waveStart, addedEffect);
                    break;
                case EffectType.WaveEnd:
                    addedLocation = checkedScript.effects.waveEnd;
                    checkedScript.effects.waveEnd = AddItemToArray(checkedScript.effects.waveEnd, addedEffect);
                    break;
                case EffectType.PPassive:
                    addedLocation = checkedScript.effects.pPassive;
                    checkedScript.effects.pPassive = AddItemToArray(checkedScript.effects.pPassive, addedEffect);
                    break;
                case EffectType.PDestroy:
                    addedLocation = checkedScript.effects.pDestroy;
                    checkedScript.effects.pDestroy = AddItemToArray(checkedScript.effects.pDestroy, addedEffect);
                    break;
                case EffectType.PHit:
                    addedLocation = checkedScript.effects.pHit;
                    checkedScript.effects.pHit = AddItemToArray(checkedScript.effects.pHit, addedEffect);
                    break;
                case EffectType.PSummon:
                    addedLocation = checkedScript.effects.pSummon;
                    checkedScript.effects.pSummon = AddItemToArray(checkedScript.effects.pSummon, addedEffect);
                    break;
                default:
                    break;
            }
            return;
        }

        relativeDuration = duration;

        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            RemoveItemFromArray(addedLocation, referenceEnR);
        }

        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyTEffect(baseTowerScript script)
    {
        if (OGValue == 0)
        {
            sumObj_Base checkedScript = script;
            switch (effectType)
            {
                case EffectType.Passive:
                    addedLocation = checkedScript.effects.passive;
                    checkedScript.effects.passive = AddItemToArray(checkedScript.effects.passive, addedEffect);
                    break;
                case EffectType.Destroy:
                    addedLocation = checkedScript.effects.destroy;
                    checkedScript.effects.destroy = AddItemToArray(checkedScript.effects.destroy, addedEffect);
                    break;
                case EffectType.Summon:
                    addedLocation = checkedScript.effects.summon;
                    checkedScript.effects.summon = AddItemToArray(checkedScript.effects.summon, addedEffect);
                    break;
                case EffectType.Hit:
                    addedLocation = checkedScript.effects.hit;
                    checkedScript.effects.hit = AddItemToArray(checkedScript.effects.hit, addedEffect);
                    break;
                case EffectType.WaveStart:
                    addedLocation = checkedScript.effects.waveStart;
                    checkedScript.effects.waveStart = AddItemToArray(checkedScript.effects.waveStart, addedEffect);
                    break;
                case EffectType.WaveEnd:
                    addedLocation = checkedScript.effects.waveEnd;
                    checkedScript.effects.waveEnd = AddItemToArray(checkedScript.effects.waveEnd, addedEffect);
                    break;
                case EffectType.PPassive:
                    addedLocation = checkedScript.effects.pPassive;
                    checkedScript.effects.pPassive = AddItemToArray(checkedScript.effects.pPassive, addedEffect);
                    break;
                case EffectType.PDestroy:
                    addedLocation = checkedScript.effects.pDestroy;
                    checkedScript.effects.pDestroy = AddItemToArray(checkedScript.effects.pDestroy, addedEffect);
                    break;
                case EffectType.PHit:
                    addedLocation = checkedScript.effects.pHit;
                    checkedScript.effects.pHit = AddItemToArray(checkedScript.effects.pHit, addedEffect);
                    break;
                case EffectType.PSummon:
                    addedLocation = checkedScript.effects.pSummon;
                    checkedScript.effects.pSummon = AddItemToArray(checkedScript.effects.pSummon, addedEffect);
                    break;
                default:
                    break;
            }
        }

        relativeDuration = duration;

        if (frameCount >= relativeDuration * Application.targetFrameRate)
        {
            RemoveItemFromArray(addedLocation, referenceEnR);
        }

        frameCount += gameControllerScript.instance.timeManager.currentTime;
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {

    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {

    }
    EffectAndReq[] AddItemToArray(EffectAndReq[] CheckedEnR, SOC_Effect AddedEffect)
    {
        //created because i don't wanna type the same shit 10 times
        List<EffectAndReq> returnedEffect = CheckedEnR.ToList<EffectAndReq>();
        EffectAndReq newEnR = new EffectAndReq();
        newEnR.effect = Instantiate(addedEffect);
        newEnR.req = new SOC_Requirement[] { };
        returnedEffect.Add(newEnR);
        referenceEnR = newEnR;
        return returnedEffect.ToArray();
    }
    EffectAndReq[] RemoveItemFromArray(EffectAndReq[] CheckedEnR, EffectAndReq RemovedEnR)
    {
        //created because i don't wanna type the same shit 10 times
        List<EffectAndReq> returnedEffect = CheckedEnR.ToList<EffectAndReq>();
        returnedEffect.Remove(RemovedEnR);
        return returnedEffect.ToArray();
    }
}
