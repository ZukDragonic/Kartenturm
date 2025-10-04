using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckBuilderScript : MonoBehaviour
{
    public gameControllerScript gameController;
    public GameObject deckCardPrefab;
    public GameObject displayArea;
    public int selectLimit;

    [HideInInspector]
    public SOCard[] allCardList;
    [HideInInspector]
    public List<SOCard> selectedCard;

    void Start()
    {
        gameController = gameControllerScript.instance;
        gameObject.transform.SetParent(gameController.allCanvas[3].transform);

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
            if(gameController.deckManager.gameDeck.Contains(currentOptionScript.card))
            {
                currentOptionScript.ChangeCosmetic(false);
                currentOptionScript.isSelected = true;
            }
            currentOptionScript.parent = this;
            currentOption.GetComponent<SOCardDisplayScript>().card = cardPool[i];
        }
    }

    public void CardSelected(bool isSelected, SOCard card)
    {
        if (isSelected)
        {
            if(selectedCard.Count < 10)
            {
                gameController.deckManager.startDeck.Add(card);
            }
            gameController.deckManager.gameDeck.Add(card);
            selectedCard.Add(card);
        }
        else
        {
            if (selectedCard.Count < 10)
            {
                gameController.deckManager.startDeck.Remove(card);
            }
            gameController.deckManager.gameDeck.Remove(card);
            selectedCard.Remove(card);
        }
    }
}
