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

    public List<SOCard> gameDeck;
    public List<SOCard> startDeck;

    public playerData(gameControllerScript gameManager)
    {
        flagDict = gameManager.flagDict;

        premiumGold = gameManager.resourceManager.premiumGold;
        seenEnemies = gameManager.resourceManager.seenEnemies;
        unlockedCards = gameManager.resourceManager.unlockedCards;
        gameDeck = gameManager.deckManager.gameDeck;
        startDeck = gameManager.deckManager.startDeck;
    }
}
