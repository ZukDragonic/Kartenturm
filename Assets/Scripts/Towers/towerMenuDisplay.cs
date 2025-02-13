using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class towerMenuDisplay : MonoBehaviour
{
    public baseTowerScript parentTower;

    public TextMeshProUGUI attackValue;
    public TextMeshProUGUI range;
    public TextMeshProUGUI pierce;
    public TextMeshProUGUI secondsPerAttack;

    public TextMeshProUGUI targetting;

    void Start()
    {
        cardsHasChanged();
        StartCoroutine(targettingChanged());
    }

    void cardsHasChanged()
    {
        float bonusStat = 0;
        foreach (var slot in parentTower.stackedCards)
        {
            bonusStat += slot.attackValue / 2 * parentTower.baseCard.multiplier;
        }
        attackValue.text = "Attack: " + parentTower.baseCard.attackValue.ToString() + " (+" +
            bonusStat.ToString() + ")";

        bonusStat = 0;
        foreach (var slot in parentTower.stackedCards)
        {
            bonusStat += slot.range / 2 * parentTower.baseCard.multiplier;
        }
        range.text = "Range: " + parentTower.baseCard.range.ToString() + " (+" +
            bonusStat + ")";

        bonusStat = 0;
        foreach (var slot in parentTower.stackedCards)
        {
            bonusStat += slot.projectilePierce / 2 * parentTower.baseCard.multiplier;
        }
        pierce.text = "Pierce: " + parentTower.baseCard.projectilePierce.ToString() + " (+" +
            bonusStat.ToString() + ")";

        secondsPerAttack.text = "Seconds Per Attack: " + parentTower.baseCard.secondsPerAttack.ToString();
    }

    IEnumerator targettingChanged()
    {
        yield return null;
        targetting.text = parentTower.currentTargetting.TargettingCheck();
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
        parentTower.GetComponent<baseTowerScript>().isSelected = false;
        gameControllerScript.instance.UpdateGameState(gameControllerScript.GameState.Wave);
    }
}
