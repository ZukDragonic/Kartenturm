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
        if (multiplier == -1)
        {
            multiplier = Random.Range(5, 16) / 10f;
        }
        multiplierReference = multiplier;

        //creates shop cards

        //first creates tower random
        int towerRan = Random.Range(0, buyNum + 1);
        for (int i = 0; i < towerRan; i++)
        {
            GameObject current = Instantiate(shopCardPrefab, buyArea);
            SOCard soldCard = gameController.deckManager.allTowers[Random.Range(0, gameController.deckManager.allTowers.Length)];
            current.GetComponent<SOCardDisplayScript>().card = soldCard;
            current.GetComponent<CBuyScript>().shopGold = current.GetComponent<SOCardDisplayScript>().card.shopGold;
            current.GetComponent<CBuyScript>().multiplier = multiplier;
        }
        

        //then artifact shop cards are created
        //subtraction is needed to exclude tower cards
        int artifactRan = Random.Range(0, buyNum + 1 - towerRan);
        for (int i = 0; i < artifactRan; i++)
        {
            GameObject current = Instantiate(shopCardPrefab, buyArea);
            SOCard soldCard = gameController.deckManager.allArtifacts[Random.Range(0, gameController.deckManager.allArtifacts.Length)];
            current.GetComponent<SOCardDisplayScript>().card = soldCard;
            current.GetComponent<CBuyScript>().shopGold = current.GetComponent<SOCardDisplayScript>().card.shopGold;
            current.GetComponent<CBuyScript>().multiplier = multiplier;
        }

        //then spell shop cards are created
        //subtraction is needed to exclude prev cards
        int spellRan = Random.Range(0, buyNum + 1 - towerRan - artifactRan);
        for (int i = 0; i < spellRan; i++)
        {
            GameObject current = Instantiate(shopCardPrefab, buyArea);
            SOCard soldCard = gameController.deckManager.allArtifacts[Random.Range(0, gameController.deckManager.allArtifacts.Length)];
            current.GetComponent<SOCardDisplayScript>().card = soldCard;
            current.GetComponent<CBuyScript>().shopGold = current.GetComponent<SOCardDisplayScript>().card.shopGold;
            current.GetComponent<CBuyScript>().multiplier = multiplier;
        }

        //fills remaining slot with random cards
        int leftSlots = buyNum - towerRan - artifactRan - spellRan;
        for (int i = 0; i < leftSlots; i++)
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
        refreshNum += 1;
        createShop(-1);
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
