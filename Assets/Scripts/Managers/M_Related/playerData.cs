using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class playerData
{
    public int saveVersion = 0001;

    public Dictionary<int, bool> flagDict;

    public int premiumGold;

    public List<int> seenEnemies;
    public List<int> unlockedCards;

    public List<int> gameDeck;
    public List<int> startDeck;

    public playerData(gameControllerScript gameManager)
    {
        flagDict = gameManager.flagDict;

        premiumGold = gameManager.resourceManager.premiumGold;
        seenEnemies = gameManager.resourceManager.seenEnemies;
        unlockedCards = gameManager.resourceManager.unlockedCards;
        gameDeck = new List<int>();
        startDeck = new List<int>();
        foreach(SOCard card in gameManager.deckManager.gameDeck)
        {
            gameDeck.Add(card.ID);
        }
        foreach (SOCard card in gameManager.deckManager.startDeck)
        {
            startDeck.Add(card.ID);
        }
    }
}
