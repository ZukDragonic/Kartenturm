using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class encyclopediaScript : MonoBehaviour
{
    public GameObject entryPrefab;
    public GameObject cEntryPrefab;
    public GameObject cImagePrefab;

    public GameObject displayArea;

    public Sprite undiscoveredImage;

    public void CreateEnemyEncyclopedia()
    {
        int[] seenEnemies = gameControllerScript.instance.resourceManager.seenEnemies.ToArray();
        SOEnemy[] allEnemies = gameControllerScript.instance.waveManager.allEnemies;
        int encyclopediaLength = allEnemies.Length;
        for (int i = 0; i < encyclopediaLength; i++)
        {
            GameObject current = Instantiate(entryPrefab, displayArea.transform);
            if (seenEnemies.Contains(allEnemies[i].ID))
            {
                current.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allEnemies[i].description;
                current.transform.GetChild(1).GetComponent<Image>().sprite = allEnemies[i].image;
            }
            else
            {
                current.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allEnemies[i].undiscoveredDes;
                current.transform.GetChild(1).GetComponent<Image>().sprite = undiscoveredImage;
            }
        }

        snapScrollScript currentSnap = transform.GetChild(0).GetComponent<snapScrollScript>();
    }

    public void CreateCardEncyclopedia()
    {
        int[] seenCards = gameControllerScript.instance.resourceManager.unlockedCards.ToArray();
        SOCard[] allCards = gameControllerScript.instance.deckManager.allCards;
        int encyclopediaLength = allCards.Length;
        int cardCount = 0;


        GameObject currentCEntry = null;
        SOCard prevCard = null;
        for (int i = 0; i < encyclopediaLength; i++)
        {
            SOCard currentCard = allCards[i];
            if (prevCard != null)
            {
                if (currentCard.ID - prevCard.ID > 1 || currentCard.ID - prevCard.ID < -1)
                {
                    cardCount = 0;
                }
            }
            prevCard = currentCard;

            if (cardCount % 24 == 0)
            {
                currentCEntry = Instantiate(cEntryPrefab, displayArea.transform);
            }
            cardCount++;

            GameObject current = Instantiate(cImagePrefab, currentCEntry.transform);

            current.GetComponent<encyclopediaCardScript>().card = currentCard;
            if (seenCards.Contains(currentCard.ID))
            {
                current.GetComponent<Image>().sprite = currentCard.image;
            }
            else
            {
                current.GetComponent<Image>().sprite = undiscoveredImage;
            }
        }
    }
}
