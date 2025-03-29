using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class shopScript : MonoBehaviour
{
    private gameControllerScript gameController;

    public float multiplierReference;

    public Transform buyArea;
    public GameObject shopCardPrefab;
    public GameObject randomCardPrefab;
    public int baseRanCost;
    public int buyNum;

    private GridLayoutGroup buyGrid;
    public Vector2 buyGridSize;
    public Vector2 buyGridSpacing;

    public TextMeshProUGUI[] refreshText;
    public int refreshNum;
    public int costIncrement;

    public void createShop(float multiplier)
    {
        //maybe ill change this if i wanna remove grid from the prefab
        //allows for refreshing of shop by recreating grid
        if (buyArea.TryGetComponent<GridLayoutGroup>(out GridLayoutGroup comp))
        {
            buyGrid = comp;
            buyGridSize = buyGrid.cellSize;
            buyGridSpacing = buyGrid.spacing;
        }
        else
        {
            buyGrid = buyArea.gameObject.AddComponent<GridLayoutGroup>();
            buyGrid.cellSize = buyGridSize;
            buyGrid.spacing = buyGridSpacing;
            buyGrid.childAlignment = TextAnchor.MiddleCenter;
        }

        //sets a random cost multiplier for shop if -1
        gameController = gameControllerScript.instance;
        gameController.UpdateGameState(GameState.ShopMenu);
        if (multiplier == -1)
        {
            multiplier = Random.Range(5, 16) / 10f;
        }
        multiplierReference = multiplier;

        //creates shop cards
        int ran = Random.Range(1, 3);
        for (int i = 0; i < buyNum - ran; i++)
        {
            GameObject current = Instantiate(shopCardPrefab, buyArea);
            SOCard soldCard = gameController.deckManager.allCards[Random.Range(0, gameController.deckManager.allCards.Length)];
            current.GetComponent<SOCardDisplayScript>().card = soldCard;
            current.GetComponent<CBuyScript>().shopGold = current.GetComponent<SOCardDisplayScript>().card.shopGold;
            current.GetComponent<CBuyScript>().multiplier = multiplier;
        }
        for (int i = 0; i < ran; i++)
        {
            GameObject current = Instantiate(randomCardPrefab, buyArea);
            CBuyScript ranCard = current.GetComponent<CBuyScript>();
            ranCard.shopGold = baseRanCost;
            ranCard.multiplier = multiplier;
        }
        StartCoroutine(destroyNextFrame());

        foreach (TextMeshProUGUI textComp in refreshText)
        {
            textComp.text = "Refresh Shop (" + (1000 + (refreshNum * costIncrement)).ToString() + ")";
        }
    }

    public void RefreshShop()
    {
        if (gameController.resourceManager.playerGold < (1000 + (refreshNum * costIncrement)))
        {
            return;
        }
        gameController.resourceManager.changeGold((1000 + (refreshNum * costIncrement)) * -1);
        int cCount = buyArea.childCount;
        for (int i = 0; i < cCount; i++)
        {
            Destroy(buyArea.GetChild(i).gameObject);
        }
        createShop(-1);
        refreshNum += 1;
    }

    IEnumerator destroyNextFrame()
    {
        yield return null;
        Destroy(buyGrid);
    }

    private void OnDestroy()
    {
        gameController.UpdateGameState(GameState.Previous);
    }
}
