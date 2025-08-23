using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class playerData
{
    public Dictionary<int, bool> flagDict;

    public int premiumGold;

    public playerData(gameControllerScript gameManager)
    {
        flagDict = gameManager.flagDict;

        premiumGold = gameManager.resourceManager.premiumGold;
    }
}
