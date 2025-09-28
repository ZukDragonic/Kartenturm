using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addDeckToHandScript : MonoBehaviour
{
    private gameControllerScript gameController;
    private deckManagerScript deckManager;

    public GameObject deckDisplayCardPrefab;
    public GameObject displayArea;
    private void Start()
    {
        gameController = gameControllerScript.instance;
        deckManager = gameController.deckManager;
        foreach(SOCard card in deckManager.startDeck)
        {
            GameObject current = Instantiate(deckDisplayCardPrefab, displayArea.transform);
            current.GetComponent<SOCardDisplayScript>().displayCard(card);
        }
    }
}
