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
    public bool placeInDeck;
    [HideInInspector]
    public bool deleteCardFromDeck;

    public List<SOCard> selectedCard;
    void Start()
    {
        gameController = gameControllerScript.instance;
        allCardList = gameController.deckManager.allCards;
        gameObject.transform.SetParent(gameController.allCanvas[3].transform);

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

    public void DisplayPickableCard(SOCard[] cardPool, int optionsNum, bool isRandom)
    {
        allCardList = cardPool;

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
        if (deleteCardFromDeck)
        {

        }
        if (placeInDeck)
        {

        }
        else
        {
            foreach(SOCard card in selectedCard)
            {
                gameController.deckManager.AddCardToHand(card);
            }
        }
        Destroy(gameObject);
    }
}
