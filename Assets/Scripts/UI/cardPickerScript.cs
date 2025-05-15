using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardPickerScript : MonoBehaviour
{
    public gameControllerScript gameController;
    public SOCard[] allCardList;
    public GameObject cardOptionPrefab;
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

    public void DisplayPickableCard(SOCard[] cardPool, int optionsNum, bool placeInDeck, bool deleteCardFromDeck)
    {
        allCardList = cardPool;

        gameController = gameControllerScript.instance;

        if (optionsNum <= 0)
        {
            optionsNum = cardPool.Length;
        }

        if (!deleteCardFromDeck)
        {
            //if it doesn't remove card from deck, it is random
            for (int i = 0; i < optionsNum; i++)
            {
                GameObject currentOption = Instantiate(cardOptionPrefab, gameObject.transform);
                cardOptionScript currentOptionScript = currentOption.GetComponent<cardOptionScript>();
                currentOptionScript.parent = this;
                currentOptionScript.placeCardInDeck = placeInDeck;
                currentOptionScript.deleteCardFromDeck = deleteCardFromDeck;
                currentOption.GetComponent<SOCardDisplayScript>().card = cardPool[Random.Range(0, cardPool.Length)];
            }
        }
        else
        {
            //if it removes card from deck, then it displays the top cards
            for (int i = 0; i < optionsNum; i++)
            {
                GameObject currentOption = Instantiate(cardOptionPrefab, gameObject.transform);
                cardOptionScript currentOptionScript = currentOption.GetComponent<cardOptionScript>();
                currentOptionScript.parent = this;
                currentOptionScript.placeCardInDeck = placeInDeck;
                currentOptionScript.deleteCardFromDeck = deleteCardFromDeck;
                currentOption.GetComponent<SOCardDisplayScript>().card = cardPool[i];
            }
        }
    }
}
