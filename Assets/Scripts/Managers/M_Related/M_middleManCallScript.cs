using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M_middleManCallScript : MonoBehaviour, IPointerDownHandler
{
    public Managers managerCalled;
    public string voidCalled;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        gameControllerScript.instance.middleMan(managerCalled, voidCalled);
    }
}