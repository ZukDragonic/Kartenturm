using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardOptionScript : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    private CCosmeticScript cosmetic;
    public cardPickerScript parent;

    public bool placeCardInDeck;
    public bool deleteCardFromDeck;

    void Start()
    {
        cosmetic = GetComponent<CCosmeticScript>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        SOCard selectedCard = gameObject.GetComponent<SOCardDisplayScript>().card;
        if (placeCardInDeck)
        {
            gameControllerScript.instance.deckManager.shuffleCardInDeck(selectedCard);
        }
        else
        {
            gameControllerScript.instance.deckManager.AddCardToHand(selectedCard);
        }

        if (deleteCardFromDeck)
        {
            gameControllerScript.instance.deckManager.drawDeck.Remove(selectedCard);
        }
        Destroy(parent.gameObject);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        cosmetic.resetCard();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        cosmetic.cosmeticColor(new Color(1.0f, 1.0f, 0.5f, 1.0f));
    }
}
