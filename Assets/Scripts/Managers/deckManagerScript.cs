using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class deckManagerScript : MonoBehaviour
{
    public gameControllerScript gameController;
    public SOCTower[] allTowers;
    public SOCArtifact[] allArtifacts;
    public SOCSpell[] allSpells;
    public SOCard nullCardReference;
    //[HideInInspector]
    public SOCard[] allCards;
    public List<SOCard> gameDeck; //the deck that is randomized in shop, picker, etc
    public List<SOCard> startDeck; //the starting deck
    [HideInInspector]
    public List<SOCard> drawDeck;
   

    public GameObject shop;
    public GameObject cardPickerPrefab;

    public GameObject TowerCardPrefab;
    public GameObject SpellCardPrefab;
    public GameObject ArtifactCardPrefab;
    private GameObject playerHand;

    public int shopWave;

    public void StartAction()
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
        drawDeck = startDeck.ToList();
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
            //DestroyImmediate is not used due to stopping animation and that creates a bug
            Destroy(gameController.allCanvas[1].transform.GetChild(0).gameObject);
        }

        StartCoroutine(OpenWhenSafe());
    }

    IEnumerator OpenWhenSafe()
    {
        yield return new WaitUntil(() => gameController.allCanvas[1].transform.childCount < 1);
        shopScript current = Instantiate(shop, gameController.allCanvas[1].transform).GetComponentInChildren<shopScript>();
        current.createShop(-1);
        gameController.UpdateGameState(GameState.ShopMenu);
        gameController.timeManager.pauseTime();
    }

    public void SearchCard(int cardsSearched, Location og, Location end)
    {
        if (gameController.allCanvas[1].transform.childCount > 0)
        {
            //DestroyImmediate destroy on same frame
            DestroyImmediate(gameController.allCanvas[1].transform.GetChild(0).gameObject);
        }

        GameObject current = Instantiate(cardPickerPrefab, gameController.allCanvas[2].transform);
        cardPickerScript cScript = current.GetComponent<cardPickerScript>();
        List<SOCard> tempCards = new List<SOCard>();
        switch (og)
        {
            case Location.Deck:
                tempCards = drawDeck.ToList();
                break;
            case Location.Field:
                foreach (baseTowerScript tower in gameController.resourceManager.aliveTower)
                {
                    tempCards.Add(tower.baseCard);
                }
                break;
            default:
                break;
        }
        cScript.DisplayPickableCard(tempCards.ToArray(), -1, cardsSearched, false);
        cScript.deleteOGCard = true;
        cScript.originalLocation = og;
        cScript.endLocation = end;
    }

    public void RemoveCard(SOCard card)
    {
        if(drawDeck.Contains(card))
        {
            drawDeck.Remove(card);
            gameController.resourceManager.UpdateDeckCount(drawDeck.Count);
        }
        else
        {
            Debug.Log("Card does not exist.");
        }
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
        gameController.resourceManager.discoverCard(card); //remove once shop is added
    }
}
