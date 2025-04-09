using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class slotParentScriptUGUI : MonoBehaviour
{
    public baseTowerScript parentTower;
    private slotChildDisplayScript slotChildDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        slotChildDisplay = GetComponent<slotChildDisplayScript>();
        slotChildDisplay.slotNum = parentTower.baseCard.slotNum;
        List<SOCard> stacked = new List<SOCard>();
        foreach (SOCard card in parentTower.stackedCards)
        {
            stacked.Add(card);
        }
        slotChildDisplay.stackedCards = stacked;
        slotChildDisplay.cardsHasChanged();
    }

    public void cardsHasChanged()
    {
        StartCoroutine(waitOneFrame());
    }

    IEnumerator waitOneFrame()
    {
        yield return null;
        List<SOCard> stacked = new List<SOCard>();
        foreach (SOCard card in slotChildDisplay.stackedCards)
        {
            stacked.Add(card);
        }
        parentTower.stackedCards = stacked;
        transform.parent.SendMessage("cardsHasChanged");
        parentTower.GetComponent<baseTowerScript>().changeStats();
    }
}
