using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M_middleManCallScript : MonoBehaviour, IPointerDownHandler
{
    public Managers managerCalled;
    public string voidCalled;
    public int IDCalled;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        gameControllerScript.instance.MiddleMan(managerCalled, voidCalled, IDCalled);
    }
}