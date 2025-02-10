using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseOverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CCosmeticScript cosmetic;
    private Color defaultColor;
    public Color hoverColor;

    void Start()
    {
        cosmetic = GetComponent<CCosmeticScript>();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        cosmetic.cosmeticColor(hoverColor);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        cosmetic.resetCard();
    }
}
