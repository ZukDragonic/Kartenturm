using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class deckCardDisplayScript : MonoBehaviour, IPointerDownHandler
{
    public deckBuilderScript parent;
    public SOCard card;


    public bool isSelected;

    void Start()
    {
        card = GetComponent<SOCardDisplayScript>().card;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(card.cardName == "")
        {
            return;
        }
        if (parent.selectedCard.Count >= parent.selectLimit && !isSelected)
        {
            return;
        }

        isSelected = !isSelected;

        ChangeCosmetic(isSelected);
        parent.CardSelected(isSelected, card);
    }

    public void ChangeCosmetic(bool isActive)
    {
        StartCoroutine(ChangeCosLater(isActive));
    }

    IEnumerator ChangeCosLater(bool isActive)
    {
        yield return null;
        if(card.cardName == "")
        {
            yield break;
        }
        if (!isActive)
        {
            if (TryGetComponent<UIMouseDownScript>(out UIMouseDownScript component))
            {
                component.enabled = true;
                GetComponent<CCosmeticScript>().resetCard();
                GetComponent<UIMouseOverScript>().enabled = true;
            }
        }
        else
        {
            if (TryGetComponent<UIMouseDownScript>(out UIMouseDownScript component))
            {
                component.enabled = false;
                component.StopAllCoroutines();
                GetComponent<CCosmeticScript>().cosmeticColor(new Color(0.8f, 0.8f, 0.8f, 0.4f));
                GetComponent<UIMouseOverScript>().enabled = false;
            }
        }
    }
}
