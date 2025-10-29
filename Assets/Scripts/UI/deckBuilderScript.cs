using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckBuilderScript : MonoBehaviour
{
    public gameControllerScript gameController;
    public GameObject deckCardPrefab;
    public GameObject displayArea;
    public GameObject deckDisplayArea;
    public int selectLimit;

    [HideInInspector]
    public SOCard[] allCardList;
    [HideInInspector]
    public List<SOCard> selectedCard;

    void Start()
    {
        gameController = gameControllerScript.instance;
        if (allCardList.Length > 0)
        {
            return;
        }
        allCardList = gameController.deckManager.allCards;
    }

    public void DisplayDeckBuilder()
    {
        List<SOCard> cardPool = new List<SOCard>();

        foreach(SOCard card in gameController.deckManager.allCards)
        {
            if(gameController.resourceManager.unlockedCards.Contains(card.ID))
            {
                cardPool.Add(card);
            }
        }

        allCardList = cardPool.ToArray();
        gameController = gameControllerScript.instance;


        for (int i = 0; i < allCardList.Length; i++)
        {
            GameObject currentOption = Instantiate(deckCardPrefab, displayArea.transform);
            deckCardDisplayScript currentOptionScript = currentOption.GetComponent<deckCardDisplayScript>();
            SOCardDisplayScript currentOptionDisplayScript = currentOption.GetComponent<SOCardDisplayScript>();
            currentOptionDisplayScript.displayCard(cardPool[i]);
            currentOptionScript.card = cardPool[i];
            //if the deck contains card, display it on left side deckbuilder
            if (gameController.deckManager.gameDeck.Contains(currentOptionScript.card))
                /*&& currentOptionScript.card != gameController.deckManager.nullCardReference)*/
            {
                int indexInDeck = gameController.deckManager.gameDeck.IndexOf(cardPool[i]);
                deckDisplayArea.transform.GetChild(indexInDeck).GetComponent<SOCardDisplayScript>().displayCard(cardPool[i]);
                currentOptionScript.ChangeCosmetic(true);
                currentOptionScript.isSelected = true;
                selectedCard.Add(cardPool[i]);
            }
            currentOptionScript.parent = this;
        }
        for (int i = 0; i < deckDisplayArea.transform.childCount; i++)
        {
            deckCardDisplayScript DCDScript = deckDisplayArea.transform.GetChild(i).GetComponent<deckCardDisplayScript>();
            DCDScript.parent = this;
            DCDScript.card = DCDScript.gameObject.GetComponent<SOCardDisplayScript>().card;
            if(DCDScript.card.cardName != "")
            {
                DCDScript.isSelected = true;
            }
            else
            {
                deckDisplayArea.transform.GetChild(i).GetComponent<UIMouseDownScript>().enabled = false;
                deckDisplayArea.transform.GetChild(i).GetComponent<UIMouseOverScript>().enabled = false;
            }
        }
    }

    public void CardSelected(bool isSelected, SOCard card)
    {
        int indexInDeck = 0;
        if (isSelected)
        {
            for(int i = 0; i < gameController.deckManager.gameDeck.Count; i++)
            {
                if(gameController.deckManager.gameDeck[i] == gameController.deckManager.nullCardReference)
                {
                    indexInDeck = i;
                    break;
                }
            }
            if(indexInDeck < 10)
            {
                gameController.deckManager.startDeck[indexInDeck] = card;
            }
            gameController.deckManager.gameDeck[indexInDeck] = card;
            GameObject deckDisplayChild = deckDisplayArea.transform.GetChild(indexInDeck).gameObject;
            deckDisplayChild.GetComponent<deckCardDisplayScript>().card = card;
            deckDisplayChild.GetComponent<deckCardDisplayScript>().isSelected = true;
            deckDisplayChild.GetComponent<SOCardDisplayScript>().displayCard(card);
            deckDisplayChild.GetComponent<UIMouseDownScript>().enabled = true;
            deckDisplayChild.GetComponent<UIMouseOverScript>().enabled = true;
            selectedCard.Add(card);
        }
        else
        {
            indexInDeck = gameController.deckManager.gameDeck.IndexOf(card);
            if (indexInDeck < 10)
            {
                gameController.deckManager.startDeck[indexInDeck] = gameController.deckManager.nullCardReference;
            }
            GameObject deckDisplayChild = deckDisplayArea.transform.GetChild(indexInDeck).gameObject;
            gameController.deckManager.gameDeck[indexInDeck] = gameController.deckManager.nullCardReference;
            deckDisplayChild.GetComponent<SOCardDisplayScript>().displayCard(gameController.deckManager.nullCardReference);
            UIMouseDownScript DDCUIMouseDown = deckDisplayChild.GetComponent<UIMouseDownScript>();
            DDCUIMouseDown.StopAllCoroutines();
            DDCUIMouseDown.enabled = false;
            deckDisplayChild.GetComponent<UIMouseOverScript>().enabled = false;
            deckDisplayChild.GetComponent<deckCardDisplayScript>().card = gameController.deckManager.nullCardReference;
            foreach(deckCardDisplayScript script in displayArea.GetComponentsInChildren<deckCardDisplayScript>())
            {
                if(script.card == card)
                {
                    script.ChangeCosmetic(false);
                    script.isSelected = false;
                }
            }
            selectedCard.Remove(card);
        }
    }
}
