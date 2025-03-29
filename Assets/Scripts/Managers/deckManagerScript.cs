using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckManagerScript : MonoBehaviour
{
    public gameControllerScript gameController;
    public SOCard[] allCards;
    public List<SOCard> drawDeck;

    public GameObject shop;

    public GameObject TowerCardPrefab;
    public GameObject SpellCardPrefab;
    private GameObject playerHand;

    private void Start()
    {
        gameController = gameControllerScript.instance;
        playerHand = GameObject.Find("playerHand").gameObject;
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

    public void openShop()
    {
        if (gameController.allCanvas[1].transform.childCount > 0)
        {
            Destroy(gameController.allCanvas[1].transform.GetChild(0).gameObject);
        }
        
        shopScript current = Instantiate(shop, gameController.allCanvas[1].transform).GetComponentInChildren<shopScript>();
        current.createShop(-1);
        gameController.UpdateGameState(GameState.ShopMenu);
        gameController.timeManager.pauseTime();
    }

    public void drawCard(int drawNum)
    {
        drawDeck = gameController.deckManager.drawDeck;
        for (int i = 0; i < drawNum; i++)
        {
            if (gameController.deckManager.drawDeck.Count > 0)
            {
                SOCard drawnCard = drawDeck[0];

                AddCardToHand(drawnCard);

                gameController.deckManager.drawDeck.RemoveAt(0);
            }
        }
    }
    public void AddCardToHand(SOCard card)
    {
        GameObject current;
        if (card as SOCTower != null)
        {
            current = Instantiate(TowerCardPrefab, playerHand.transform);
        }
        else
        {
            current = Instantiate(SpellCardPrefab, playerHand.transform);
        }
        current.GetComponent<SOCardDisplayScript>().card = card;
        //add a onDraw effect here in the future maybe
    }
}
