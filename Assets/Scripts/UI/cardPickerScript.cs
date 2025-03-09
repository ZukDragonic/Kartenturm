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

    public void displayPickableCard(int options)
    {
        gameController = gameControllerScript.instance;
        allCardList = gameController.deckManager.allCards;

        for (int i = 0; i < options; i++)
        {
            GameObject currentOption = Instantiate(cardOptionPrefab, gameObject.transform);
            currentOption.GetComponent<cardOptionScript>().parent = this;
            currentOption.GetComponent<SOCardDisplayScript>().card = allCardList[Random.Range(0, allCardList.Length)];
        }
    }

    public void placeSelectedCardInDeck(SOCard card)
    {
        gameController.deckManager.shuffleCardInDeck(card);
    }
}
