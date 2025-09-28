using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cPConfirmButtonScript : MonoBehaviour, IPointerDownHandler
{
    private cardPickerScript parent;

    void Start()
    {
        parent = gameObject.transform.parent.GetComponent<cardPickerScript>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        parent.ConfirmPickedCards();
    }
}
