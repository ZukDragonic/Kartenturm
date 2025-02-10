using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class slotUGUI : MonoBehaviour, IDropHandler
{
    private SOCard droppedCard;
    
    void Start()
    {
        droppedCard = GetComponent<SOCardDisplayScript>().card;
    }
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        
        if (dropped.TryGetComponent<CIHTowerScript>(out CIHTowerScript towerScript))
        {
            droppedCard = towerScript.card;
        }
        if (dropped.TryGetComponent<CIHSpellScript>(out CIHSpellScript spellScript))
        {
            droppedCard = spellScript.card;
        }
        GetComponent<SOCardDisplayScript>().displayCard(droppedCard);
        transform.parent.GetComponent<slotChildDisplayScript>().stackedCards[transform.GetSiblingIndex()] = droppedCard;
        GetComponent<placeableAreaScript>().isPlaceable = false;
        transform.parent.SendMessage("cardsHasChanged");
    }
}
