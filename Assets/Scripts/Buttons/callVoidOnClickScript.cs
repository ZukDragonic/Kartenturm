using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class callVoidOnClickScript : MonoBehaviour, IPointerDownHandler
{
    public MonoBehaviour script;
    public string voidCalled;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        script.SendMessage(voidCalled);
    }
}
