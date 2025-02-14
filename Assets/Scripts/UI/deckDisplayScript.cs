using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class deckDisplayScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public numberDisplayScript numDisplay;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        numDisplay.gameObject.SetActive(true);
        numDisplay.updateDisplay(gameControllerScript.instance.deckManager.drawDeck.Count);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        numDisplay.gameObject.SetActive(false);
    }
}
