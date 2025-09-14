using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class encyclopediaScript : MonoBehaviour
{
    public GameObject eEntryPrefab;

    public GameObject displayArea;

    public Sprite undiscoveredImage;
    public Scrollbar scrollbar;

    public void CreateEnemyEncyclopedia()
    {
        SOEnemy[] seenEnemies = gameControllerScript.instance.resourceManager.seenEnemies.ToArray();
        SOEnemy[] allEnemies = gameControllerScript.instance.waveManager.allEnemies;
        int encyclopediaLength = allEnemies.Length;
        for (int i = 0; i < encyclopediaLength; i++)
        {
            GameObject current = Instantiate(eEntryPrefab, displayArea.transform);
            if (seenEnemies.Contains(allEnemies[i]))
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
    }

    public void CreateCardEncyclopedia()
    {
        SOCard[] seenCards = gameControllerScript.instance.resourceManager.seenCards.ToArray();
        int encyclopediaLength = seenCards.Length;
        for (int i = 0; i < encyclopediaLength;i++)
        {
            GameObject current = Instantiate(eEntryPrefab, displayArea.transform);
            current.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = seenCards[i].description;
            current.transform.GetChild(1).GetComponent<Image>().sprite = seenCards[i].image;
        }
    }
}
