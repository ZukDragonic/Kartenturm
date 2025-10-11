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
            if (gameController.deckManager.gameDeck.Contains(currentOptionScript.card))
                /*&& currentOptionScript.card != gameController.deckManager.nullCardReference)*/
            {
                int indexInDeck = gameController.deckManager.gameDeck.IndexOf(cardPool[i]);
                deckDisplayArea.transform.GetChild(indexInDeck).GetComponent<SOCardDisplayScript>().displayCard(cardPool[i]);
                currentOptionScript.ChangeCosmetic(false);
                currentOptionScript.isSelected = true;
            }
            currentOptionScript.parent = this;
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
            deckDisplayArea.transform.GetChild(indexInDeck).GetComponent<SOCardDisplayScript>().displayCard(card);
            selectedCard.Add(card);
        }
        else
        {
            indexInDeck = gameController.deckManager.gameDeck.IndexOf(card);
            if (indexInDeck < 10)
            {
                gameController.deckManager.startDeck[indexInDeck] = gameController.deckManager.nullCardReference;
            }
            gameController.deckManager.gameDeck[indexInDeck] = gameController.deckManager.nullCardReference;
            deckDisplayArea.transform.GetChild(indexInDeck).GetComponent<SOCardDisplayScript>().displayCard(gameController.deckManager.nullCardReference);
            selectedCard.Remove(card);
        }
    }
}
