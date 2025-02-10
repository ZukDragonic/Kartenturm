using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardOptionScript : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    private CCosmeticScript cosmetic;
    public cardPickerScript parent;

    void Start()
    {
        cosmetic = GetComponent<CCosmeticScript>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        parent.placeSelectedCardInDeck(gameObject.GetComponent<SOCardDisplayScript>().card);
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
