using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardOptionScript : MonoBehaviour, IPointerDownHandler
{
    private CCosmeticScript cosmetic;
    public cardPickerScript parent;
    public SOCard card;


    private bool isSelected;

    void Start()
    {
        cosmetic = GetComponent<CCosmeticScript>();
        card = gameObject.GetComponent<SOCardDisplayScript>().card;

    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(parent.selectedCard.Count >= parent.selectLimit && !isSelected)
        {
            return;
        }
        if (isSelected)
        {
            isSelected = false;
            parent.selectedCard.Remove(this);
            if (TryGetComponent<UIMouseDownScript>(out UIMouseDownScript component))
            {
                component.enabled = true;
                GetComponent<UIMouseOverScript>().enabled = true;
            }
        }
        else
        {
            isSelected = true;
            parent.selectedCard.Add(this);

            if (TryGetComponent<UIMouseDownScript>(out UIMouseDownScript component))
            {
                component.enabled = false;
                GetComponent<UIMouseOverScript>().enabled = false;
                GetComponent<CCosmeticScript>().cosmeticColor(new Color(0.8f, 0.8f, 0.8f, 0.4f));
            }
        }
    }
}
