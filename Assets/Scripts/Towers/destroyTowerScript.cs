using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTowerScript : MonoBehaviour
{
    public baseTowerScript parentTower;

    void destroyTower()
    {
        foreach (SOCard card in parentTower.stackedCards)
        {
            gameControllerScript.instance.deckManager.shuffleCardInDeck(card);
        }

        parentTower.transform.parent.GetComponent<placeableAreaScript>().isPlaceable = true;
        gameControllerScript.instance.resourceManager.removeTower(parentTower.gameObject);
        Destroy(parentTower.gameObject);
    }
}
