using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopScript : MonoBehaviour
{
    private gameControllerScript gameController;

    public float multiplierReference;

    public Transform buyArea;
    public GameObject shopCardPrefab;
    public GameObject randomCardPrefab;
    public int buyNum;

    void Start()
    {
        gameController = gameControllerScript.instance;
        createShop(-1);
    }

    void createShop(float multiplier)
    {
        gameController.UpdateGameState(gameControllerScript.GameState.GameMenu);
        if (multiplier == -1)
        {
            multiplier = Random.Range(5, 16) / 10f;
        }
        multiplierReference = multiplier;

        int ran = Random.Range(1, 3);
        for (int i = 0; i < buyNum - ran; i++)
        {
            GameObject current = Instantiate(shopCardPrefab, buyArea);
            SOCard soldCard = gameController.deckManager.allCards[Random.Range(0, gameController.deckManager.allCards.Length)];
            current.GetComponent<SOCardDisplayScript>().card = soldCard;
            current.GetComponent<CBuyScript>().gold = current.GetComponent<SOCardDisplayScript>().card.gold;
            current.GetComponent<CBuyScript>().multiplier = multiplier;
        }
        for (int i = 0; i < ran; i++)
        {
            GameObject current = Instantiate(randomCardPrefab, buyArea);
            current.GetComponent<CBuyScript>().gold = 10;
            current.GetComponent<CBuyScript>().multiplier = multiplier;
        }
        StartCoroutine(destroyNextFrame());
    }

    IEnumerator destroyNextFrame()
    {
        yield return null;
        Destroy(buyArea.GetComponent<GridLayoutGroup>());
    }

    private void OnDestroy()
    {
        gameController.UpdateGameState(gameControllerScript.GameState.Wave);
    }
}
