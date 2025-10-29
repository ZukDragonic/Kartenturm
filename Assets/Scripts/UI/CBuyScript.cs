using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CBuyScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private gameControllerScript gameController;
    private CCosmeticScript cosmetic;
    public int shopGold;
    public float multiplier;
    private int price;
    public TextMeshProUGUI pricePrefab;

    public bool isRandom;
    public GameObject cardPickerPrefab;
    void Start()
    {
        gameController = gameControllerScript.instance;
        cosmetic = gameObject.GetComponent<CCosmeticScript>();
        price = (int)(shopGold * multiplier);
        pricePrefab.text = price.ToString();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (gameController.resourceManager.playerGold >= price)
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
        if (gameController.resourceManager.playerGold >= shopGold * multiplier)
        {
            gameController.resourceManager.changeGold(price * -1);
            if (!isRandom)
            {
                gameController.deckManager.shuffleCardInDeck(GetComponent<SOCardDisplayScript>().card);
                Destroy(gameObject);
            }
            else
            {
                GameObject current = Instantiate(cardPickerPrefab, gameController.allCanvas[2].transform);
                current.GetComponent<cardPickerScript>().DisplayPickableCard(gameController.deckManager.allCards, 3, 1, true);
                current.GetComponent<cardPickerScript>().endLocation = Location.Deck;
                Destroy(gameObject);
            }
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        cosmetic.resetCard();
    }
}
