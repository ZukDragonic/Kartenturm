using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class towerMenuDisplay : MonoBehaviour
{
    public baseTowerScript parentTower;

    public TextMeshProUGUI towerName;
    public TextMeshProUGUI attackValue;
    public TextMeshProUGUI range;
    public TextMeshProUGUI pierce;
    public TextMeshProUGUI secondsPerAttack;

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

        float bonusStat = parentTower.attackValue - parentTower.baseCard.attackValue;
        attackValue.text = "Attack: " + parentTower.baseCard.attackValue.ToString() + " (+" +
            bonusStat.ToString() + ")";

        bonusStat = parentTower.range - parentTower.baseCard.range;
        range.text = "Range: " + parentTower.baseCard.range.ToString() + " (+" +
            bonusStat + ")";

        bonusStat = parentTower.projectilePierce - parentTower.baseCard.projectilePierce;
        pierce.text = "Pierce: " + parentTower.baseCard.projectilePierce.ToString() + " (+" +
            bonusStat.ToString() + ")";

        secondsPerAttack.text = "Seconds Per Attack: " + parentTower.baseCard.secondsPerAttack.ToString();
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
