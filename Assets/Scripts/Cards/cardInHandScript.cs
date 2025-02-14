using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cardInHandScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 ogPos;
    private Quaternion ogRotation;

    void Start()
    {
        ogPos = transform.position;
        ogRotation = transform.rotation;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        gameControllerScript.instance.isCardDragged = true;
        transform.position = new Vector3(ogPos.x, ogPos.y + 100, 0);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //follows mouse
        transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        gameControllerScript.instance.isCardDragged = false;
        transform.rotation = ogRotation;
        transform.position = ogPos;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        transform.position = ogPos;
        transform.rotation = ogRotation;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ogPos = transform.position;
        ogRotation = transform.rotation;
        transform.position = new Vector3(ogPos.x, ogPos.y + 100, 0);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}