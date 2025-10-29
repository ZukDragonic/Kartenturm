using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardPickerScript : MonoBehaviour
{
    public gameControllerScript gameController;
    public SOCard[] allCardList;
    public GameObject cardOptionPrefab;
    public GameObject displayArea;

    [HideInInspector]
    public Location originalLocation;
    [HideInInspector]
    public Location endLocation;
    [HideInInspector]
    public bool deleteOGCard; //might have to make this an Enum to allow it to pick from deck as well
    [HideInInspector]
    public int selectLimit;
    [HideInInspector]
    public List<cardOptionScript> selectedCard;

    void Start()
    {
        gameController = gameControllerScript.instance;
        gameObject.transform.SetParent(gameController.allCanvas[3].transform);

        if(allCardList.Length > 0)
        {
            return;
        }
        allCardList = gameController.deckManager.allCards;
        if (gameObject.transform.GetComponentsInChildren<cardOptionScript>() != null)
        {
            foreach (cardOptionScript cardOption in gameObject.transform.GetComponentsInChildren<cardOptionScript>())
            {
                cardOption.GetComponent<cardOptionScript>().parent = this;
                if (cardOption.gameObject.TryGetComponent<SOCardDisplayScript>(out SOCardDisplayScript script))
                {
                    script.card = allCardList[Random.Range(0, allCardList.Length)];
                }
            }
        }
    }

    public void DisplayPickableCard(SOCard[] cardPool, int optionsNum, int maxSelection, bool isRandom)
    {
        allCardList = cardPool;
        selectLimit = maxSelection;
        gameController = gameControllerScript.instance;

        if (optionsNum <= 0)
        {
            optionsNum = cardPool.Length;
        }

        for (int i = 0; i < optionsNum; i++)
        {
            GameObject currentOption = Instantiate(cardOptionPrefab, displayArea.transform);
            cardOptionScript currentOptionScript = currentOption.GetComponent<cardOptionScript>();
            currentOptionScript.parent = this;
            if(isRandom) //if it's random, select from every card from the game
            {
                currentOption.GetComponent<SOCardDisplayScript>().card = cardPool[Random.Range(0, cardPool.Length)];
            }
            else //if it's not random, show from the top

            {
                currentOption.GetComponent<SOCardDisplayScript>().card = cardPool[i];
            }
        }
    }

    public void ConfirmPickedCards()
    {
        if(selectedCard.Count < selectLimit)
        {
            return;
        }
        if (deleteOGCard)
        {
            switch (originalLocation)
            {
                case Location.Deck:
                    foreach (cardOptionScript option in selectedCard)
                    {
                        gameController.deckManager.RemoveCard(option.card);
                    }
                    break;
                case Location.Field:
                    foreach (cardOptionScript option in selectedCard)
                    {
                        int childIndex = option.transform.GetSiblingIndex();
                        gameController.resourceManager.removeTower(gameController.resourceManager.aliveTower[childIndex]);
                    }
                    break;
                default:
                    break;
            }
        }
        switch (endLocation)
        {
            case Location.Deck:
                foreach (cardOptionScript option in selectedCard)
                {
                    gameController.deckManager.shuffleCardInDeck(option.card);
                }
                break;
            case Location.Hand:
                foreach (cardOptionScript option in selectedCard)
                {
                    gameController.deckManager.AddCardToHand(option.card);
                }
                break;
            case Location.Gold:
                foreach (cardOptionScript option in selectedCard)
                {
                    gameController.resourceManager.changeGold(option.card.actionGold);
                }
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
}
