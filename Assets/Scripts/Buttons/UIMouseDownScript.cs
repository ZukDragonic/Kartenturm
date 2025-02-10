using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMouseDownScript : MonoBehaviour, IPointerDownHandler
{
    private CCosmeticScript cosmetic;
    private Color defaultColor;
    public Color downColor;

    void Start()
    {
        cosmetic = GetComponent<CCosmeticScript>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        defaultColor = GetComponent<Image>().color;
        cosmetic.cosmeticColor(downColor);
        StartCoroutine(resetColor());
    }

    IEnumerator resetColor()
    {
        yield return new WaitForSeconds(0.3f);
        cosmetic.cosmeticColor(defaultColor);
    }
}