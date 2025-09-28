using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class encyclopediaCardScript : MonoBehaviour, IPointerDownHandler
{
    public GameObject cardDescriptionPrefab;

    [HideInInspector]
    public SOCard card;
    private bool isUnlocked;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        GameObject current = Instantiate(cardDescriptionPrefab, gameControllerScript.instance.allCanvas[1].transform);

        if(Camera.main.ScreenToViewportPoint(gameObject.transform.position).x < 0.5f)
        {
            current.transform.localPosition *= -1;
        }

        Sprite currentImage = GetComponent<Image>().sprite;
        current.transform.GetChild(2).GetComponent<Image>().sprite = currentImage;
        if(currentImage == card.image)
        {
            current.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = card.cardName;
            if(card as SOCTower != null)
            {
                current.transform.GetChild(4).gameObject.SetActive(false);
                current.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Damage: " + (card as SOCTower).damage.ToString();
                current.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Range: " + (card as SOCTower).range.ToString();
                current.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Pierce: " + (card as SOCTower).pierce.ToString();
                current.transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>().text = "SPA: " + (card as SOCTower).secondsPerAttack.ToString();
            }
            else
            {
                current.transform.GetChild(3).gameObject.SetActive(false);
                if(card as SOCArtifact != null)
                {
                    current.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Artifact";
                }
                else
                {
                    current.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Spell";
                }
            }
            current.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = card.description;
        }
        else
        {
            current.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "???";
            current.transform.GetChild(3).gameObject.SetActive(false);
            current.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "???";
            current.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "???";
        }
    }
}