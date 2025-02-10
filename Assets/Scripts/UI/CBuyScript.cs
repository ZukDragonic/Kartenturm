using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CBuyScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private gameControllerScript gameController;
    private CCosmeticScript cosmetic;
    public int gold;
    public float multiplier;
    private int price;
    public TextMeshProUGUI pricePrefab;

    public bool isRandom;
    public GameObject cardPickerPrefab;
    void Start()
    {
        gameController = gameControllerScript.instance;
        cosmetic = gameObject.GetComponent<CCosmeticScript>();
        price = (int)(gold * multiplier);
        pricePrefab.text = price.ToString();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (gameController.resourceManager.gold >= price)
        {
            cosmetic.cosmeticColor(new Color(1, 1, 0.5f, 1));
        }
        else
        {
            cosmetic.cosmeticColor(new Color(1, 0.5f, 0.5f, 1));
        }
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (gameController.resourceManager.gold >= gold * multiplier)
        {
            gameController.resourceManager.changeGold(price * -1);
            if (!isRandom)
            {
                gameController.deckManager.shuffleCardInDeck(GetComponent<SOCardDisplayScript>().card);
                Destroy(gameObject);
            }
            else
            {
                GameObject current = Instantiate(cardPickerPrefab, gameController.topCanvas.transform);
                current.GetComponent<cardPickerScript>().displayPickableCard(3);
                Destroy(gameObject);
            }
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        cosmetic.resetCard();
    }
}
