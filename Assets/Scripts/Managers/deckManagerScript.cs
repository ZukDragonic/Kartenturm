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
    public void shuffleDeck()
    {
        int drawCount = drawDeck.Count;
        for (int i = drawCount - 1; i > 0; i--)
        {
            //randomly grabs an item from list 0 to 1
            int j = Random.Range(0, i);

            //swaps the item j and i
            SOCard temp = drawDeck[j];
            drawDeck[j] = drawDeck[i];
            drawDeck[i] = temp;
        }
        //play animation here
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
        shopScript current = Instantiate(shop, gameController.allCanvas[1].transform).GetComponent<shopScript>();
        current.createShop(-1);
        gameController.timeManager.pauseTime();
    }
}
