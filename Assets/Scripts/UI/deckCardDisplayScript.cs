using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class deckCardDisplayScript : MonoBehaviour, IPointerDownHandler
{
    public deckBuilderScript parent;
    private CCosmeticScript cosmetic;
    public SOCard card;


    public bool isSelected;

    void Start()
    {
        cosmetic = GetComponent<CCosmeticScript>();
        card = gameObject.GetComponent<SOCardDisplayScript>().card;

    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (parent.selectedCard.Count >= parent.selectLimit && !isSelected)
        {
            return;
        }

        ChangeCosmetic(isSelected);
        if (isSelected)
        {
            isSelected = false;
        }
        else
        {
            isSelected = true;
        }
        parent.CardSelected(isSelected, card);
    }

    public void ChangeCosmetic(bool isActive)
    {
        if (isActive)
        {
            if (TryGetComponent<UIMouseDownScript>(out UIMouseDownScript component))
            {
                component.enabled = true;
                GetComponent<UIMouseOverScript>().enabled = true;
            }
        }
        else
        {
            if (TryGetComponent<UIMouseDownScript>(out UIMouseDownScript component))
            {
                component.enabled = false;
                GetComponent<UIMouseOverScript>().enabled = false;
                GetComponent<CCosmeticScript>().cosmeticColor(new Color(0.8f, 0.8f, 0.8f, 0.4f));
            }
        }
    }
}
