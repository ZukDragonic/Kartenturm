using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class towerMenuDisplay : MonoBehaviour
{
    public baseTowerScript parentTower;

    public TextMeshProUGUI towerName;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI range;
    public TextMeshProUGUI pierce;
    public TextMeshProUGUI secondsPerAttack;

    public Transform effectDisplay;
    public GameObject textPrefab;

    public TextMeshProUGUI targetting;

    public GameObject targettingChild;

    void Start()
    {
        cardsHasChanged();
        StartCoroutine(targettingChanged());
    }

    void cardsHasChanged()
    {
        StartCoroutine(WaitNextFrame());
    }
    IEnumerator WaitNextFrame()
    {
        yield return null;
        towerName.text = parentTower.baseCard.cardName;

        float bonusStat = parentTower.damage - parentTower.baseCard.damage;
        string bonusDisplay = BonusDisplay(bonusStat);
        damage.text = "Attack: " + parentTower.baseCard.damage.ToString() + bonusDisplay;

        bonusStat = parentTower.range - parentTower.baseCard.range;
        bonusDisplay = BonusDisplay(bonusStat);
        range.text = "Range: " + parentTower.baseCard.range.ToString() + bonusDisplay;

        bonusStat = parentTower.pierce - parentTower.baseCard.pierce;
        bonusDisplay = BonusDisplay(bonusStat);
        pierce.text = "Pierce: " + parentTower.baseCard.pierce.ToString() + bonusDisplay;

        bonusStat = parentTower.secondsPerAttack - parentTower.baseCard.secondsPerAttack;
        bonusDisplay = BonusDisplay(bonusStat);
        secondsPerAttack.text = "SPA: " + parentTower.baseCard.secondsPerAttack.ToString() + bonusDisplay;

        for(int i = 0; i < effectDisplay.childCount ;i++)
        {
            Destroy(effectDisplay.GetChild(i).gameObject);
        }

        foreach (EffectAndReq EnR in parentTower.currentEffect.passive)
        {
            CreateEffectDisplay(EnR.effect, EffectType.Passive);
        }
        foreach (EffectAndReq EnR in parentTower.currentEffect.destroy)
        {
            CreateEffectDisplay(EnR.effect, EffectType.Destroy);
        }
        foreach (EffectAndReq EnR in parentTower.currentEffect.summon)
        {
            CreateEffectDisplay(EnR.effect, EffectType.Summon);
        }
        foreach (EffectAndReq EnR in parentTower.currentEffect.hit)
        {
            CreateEffectDisplay(EnR.effect, EffectType.Hit);
        }
        foreach (EffectAndReq EnR in parentTower.currentEffect.waveStart)
        {
            CreateEffectDisplay(EnR.effect, EffectType.WaveStart);
        }
        foreach (EffectAndReq EnR in parentTower.currentEffect.waveEnd)
        {
            CreateEffectDisplay(EnR.effect, EffectType.WaveEnd);
        }
        foreach (EffectAndReq EnR in parentTower.currentEffect.pPassive)
        {
            CreateEffectDisplay(EnR.effect, EffectType.PPassive);
        }
        foreach (EffectAndReq EnR in parentTower.currentEffect.pDestroy)
        {
            CreateEffectDisplay(EnR.effect, EffectType.PDestroy);
        }
        foreach (EffectAndReq EnR in parentTower.currentEffect.pHit)
        {
            CreateEffectDisplay(EnR.effect, EffectType.PHit);
        }
        foreach (EffectAndReq EnR in parentTower.currentEffect.pSummon)
        {
            CreateEffectDisplay(EnR.effect, EffectType.PSummon);
        }
    }

    void CreateEffectDisplay(SOC_Effect effect, EffectType effectType)
    {
        string description = effect.EffectDescription();
        if (description == null || description == "")
        {
            return;
        }
        switch (effectType)
        {
            case EffectType.Passive:
                description = "Always: " + description;
                break;
            case EffectType.Destroy:
                description = "On Destroy: " + description;
                break;
            case EffectType.Summon:
                description = "On Summon: " + description;
                break;
            case EffectType.Hit:
                description = "On Hit: " + description;
                break;
            case EffectType.WaveStart:
                description = "On Wave Start: " + description;
                break;
            case EffectType.WaveEnd:
                description = "On Wave End: " + description;
                break;
            case EffectType.PPassive:
                description = "Spawned Objects Always: " + description;
                break;
            case EffectType.PDestroy:
                description = "On Spawned Objects Destroyed: " + description;
                break;
            case EffectType.PHit:
                description = "On Spawned Objects Hit: " + description;
                break;
            case EffectType.PSummon:
                description = "On Spawned Objects Summon: " + description;
                break;
            default:
                break;
        }
        GameObject spawnedText = Instantiate(textPrefab, effectDisplay);
        TextMeshProUGUI spawnTextComp = spawnedText.GetComponent<TextMeshProUGUI>();
        spawnTextComp.text = description;
        spawnTextComp.enableAutoSizing = true;
        spawnTextComp.rectTransform.sizeDelta = new Vector2(190, 50);
    }

    string BonusDisplay(float bonusInt)
    {
        string bonusDisplay = bonusInt.ToString();
        if (bonusInt < 0)
        {
            bonusDisplay = " (" + bonusDisplay + ")";
        }
        else if (bonusInt > 0)
        {
            bonusDisplay = " (+" + bonusDisplay + ")";
        }
        else
        {
            bonusDisplay = "";
        }
        return bonusDisplay;
    }

    IEnumerator targettingChanged()
    {
        yield return null;
        targetting.text = parentTower.currentTargetting.TargettingCheck();
        if (parentTower.allTargetting.Length == 1)
        {
            targettingChild.transform.GetChild(0).gameObject.SetActive(false);
            targettingChild.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void targetPrevious()
    {
        parentTower.changeTargetting(-1);
        StartCoroutine(targettingChanged());
    }

    void targetNext()
    {
        parentTower.changeTargetting(1);
        StartCoroutine(targettingChanged());
    }
    void OnDestroy()
    {
        parentTower.closeTowerMenu();
    }
}
