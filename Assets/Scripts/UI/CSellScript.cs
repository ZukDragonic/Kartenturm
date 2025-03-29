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

    //updates the value display
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
        //adds gold to player
        gameController.resourceManager.changeGold(sellValue);

        ResetSellarea();
    }

    public void sellCancel()
    {
        //make slotChildDisplay reset and add cards back into hand
        foreach (SOCard card in stackedCards)
        {
            if (card.cardName != "")
            {
                gameController.deckManager.AddCardToHand(card);
            }
        }
        ResetSellarea();
    }

    private void ResetSellarea()
    {
        //destroy all the slotChildDisplay
        foreach (GameObject child in slotChildDisplay.child)
        {
            Destroy(child);
        }
        slotChildDisplay.child = new List<GameObject>();
        //final step to create a new sell area
        slotChildDisplay.stackedCards = new List<SOCard>();
        //first creates new slott-able cards and then changes their display
        slotChildDisplay.initializeStackedCards();
        slotChildDisplay.cardsHasChanged();
        cardsHasChanged();
    }
}
