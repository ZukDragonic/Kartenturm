using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cardInHandScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 ogPos;
    public Quaternion ogRotation;
    private Transform ogParent;

    void Start()
    {
        ogPos = transform.localPosition;
        ogRotation = transform.localRotation;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        gameControllerScript.instance.isCardDragged = true;
        transform.position = new Vector3(ogPos.x, ogPos.y + 100, 0);
        transform.SetParent(transform.parent.parent);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //follows mouse
        transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        gameControllerScript.instance.isCardDragged = false;
        transform.SetParent(ogParent);
        transform.localRotation = ogRotation;
        transform.localPosition = ogPos;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (gameControllerScript.instance.isCardDragged)
        {
            return;
        }
        transform.localPosition = ogPos;
        transform.localRotation = ogRotation;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (gameControllerScript.instance.isCardDragged)
        {
            return;
        }
        ogPos = transform.localPosition;
        ogRotation = transform.localRotation;
        ogParent = transform.parent;
        transform.localPosition = new Vector3(ogPos.x, 100, 0);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}