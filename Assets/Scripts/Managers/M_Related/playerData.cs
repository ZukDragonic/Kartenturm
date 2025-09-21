using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class playerData
{
    public Dictionary<int, bool> flagDict;

    public int premiumGold;

    public List<int> seenEnemies;
    public List<int> unlockedCards;

    public playerData(gameControllerScript gameManager)
    {
        flagDict = gameManager.flagDict;

        premiumGold = gameManager.resourceManager.premiumGold;
        seenEnemies = gameManager.resourceManager.seenEnemies;
        unlockedCards = gameManager.resourceManager.unlockedCards;
    }
}
