using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckManagerScript : MonoBehaviour
{
    public gameControllerScript gameController;
    public SOCard[] allCards;
    public List<SOCard> drawDeck;
    private drawScript draw;

    public GameObject shop;

    private void Start()
    {
        draw = GetComponent<drawScript>();
        gameController = gameControllerScript.instance;
    }

    public void shuffleCardInDeck(SOCard card)
    {
        drawDeck.Insert(Random.Range(0, drawDeck.Count), card);
    }

    public void drawCard(int num)
    {
        draw.drawCard(num);
    }

    public void openShop()
    {
        Instantiate(shop, gameController.topCanvas.transform);
    }
}
