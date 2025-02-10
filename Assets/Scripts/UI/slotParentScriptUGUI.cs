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
        foreach (SOCTower tower in parentTower.stackedCards)
        {
            stacked.Add(tower as SOCard);
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
        List<SOCTower> stacked = new List<SOCTower>();
        foreach (SOCard tower in slotChildDisplay.stackedCards)
        {
            stacked.Add(tower as SOCTower);
        }
        parentTower.stackedCards = stacked;
        transform.parent.SendMessage("cardsHasChanged");
        parentTower.GetComponent<baseTowerScript>().changeStats();
    }
}
