using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CSellScript : MonoBehaviour
{
    private shopScript shop;
    private gameControllerScript gameController;
    private slotChildDisplayScript slotChildDisplay;

    public TextMeshProUGUI valueDisplay;

    public List<SOCard> stackedCards;
    public int sellValue;

    // Start is called before the first frame update
    void Start()
    {
        slotChildDisplay = transform.GetChild(0).GetComponent<slotChildDisplayScript>();
        shop = transform.parent.GetComponent<shopScript>();
        gameController = gameControllerScript.instance;
        slotChildDisplay.initializeStackedCards();
        slotChildDisplay.cardsHasChanged();
        cardsHasChanged();
    }


    void cardsHasChanged()
    {
        int currentValue = 0;
        stackedCards = slotChildDisplay.stackedCards;
        foreach (SOCard card in stackedCards)
        {
            currentValue += (int)(card.shopGold * 0.8f * shop.multiplierReference);
        }
        sellValue = currentValue;
        valueDisplay.text = sellValue.ToString();
    }
    //call both voids with a button
    public void sellCards()
    {
        gameController.resourceManager.changeGold(sellValue);
        
        //destroy all the slotChildDisplay and add gold to player
        foreach (GameObject child in slotChildDisplay.child)
        {
            Destroy(child);
        }
        slotChildDisplay.child = new List<GameObject>();
        //final step
        slotChildDisplay.stackedCards = new List<SOCard>();
        slotChildDisplay.initializeStackedCards();
        slotChildDisplay.cardsHasChanged();
        cardsHasChanged();
    }

    public void sellCancel()
    {
        //make slotChildDisplay reset and add cards back into hand
        stackedCards = slotChildDisplay.stackedCards;
    }
}
