using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckManagerScript : MonoBehaviour
{
    public gameControllerScript gameController;
    public SOCTower[] allTowers;
    public SOCArtifact[] allArtifacts;
    public SOCSpell[] allSpells;
    [HideInInspector]
    public SOCard[] allCards;
    public List<SOCard> drawDeck;

    public GameObject shop;

    public GameObject TowerCardPrefab;
    public GameObject SpellCardPrefab;
    public GameObject ArtifactCardPrefab;
    private GameObject playerHand;

    public int shopWave;

    private void Start()
    {
        List<SOCard> tempCards = new List<SOCard>();
        foreach (SOCard card in allTowers)
        {
            tempCards.Add(card);
        }
        foreach (SOCard card in allArtifacts)
        {
            tempCards.Add(card);
        }
        foreach (SOCard card in allSpells)
        {
            tempCards.Add(card);
        }
        allCards = tempCards.ToArray();
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
        gameController.resourceManager.UpdateDeckCount(drawDeck.Count);
    }

    public void openShop()
    {
        if (gameController.allCanvas[1].transform.childCount > 0)
        {
            //DestroyImmediate is used so that it would call previous state first and then shop state
            //DestroyImmediate destroy on same frame
            DestroyImmediate(gameController.allCanvas[1].transform.GetChild(0).gameObject);
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
        gameController.resourceManager.UpdateDeckCount(drawDeck.Count);
    }
    public void AddCardToHand(SOCard card)
    {
        GameObject current;
        if (card as SOCTower != null)
        {
            current = Instantiate(TowerCardPrefab, playerHand.transform);
        }
        else if (card as SOCSpell != null)
        {
            current = Instantiate(SpellCardPrefab, playerHand.transform);
        }
        else
        {
            current = Instantiate(ArtifactCardPrefab, playerHand.transform);
        }
        current.GetComponent<SOCardDisplayScript>().card = card;
        //add a onDraw effect here in the future maybe
        gameController.resourceManager.UpdateDeckCount(drawDeck.Count);
    }
}
