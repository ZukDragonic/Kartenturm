using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M_middleManCallScript : MonoBehaviour, IPointerDownHandler
{
    public Managers managerCalled;
    public string voidCalled;
    public int IDCalled;

    private bool isActive;
    public int IDCheckedForActive;

    void Start()
    {
        if (IDCheckedForActive == 0)
        {
            isActive = true;
        }
        else
        {
            isActive = gameControllerScript.instance.FlagInfo(IDCheckedForActive);
            if (!isActive)
            {
                if (TryGetComponent<UIMouseDownScript>(out UIMouseDownScript component))
                {
                    component.enabled = false;
                    GetComponent<UIMouseOverScript>().enabled = false;
                    GetComponent<CCosmeticScript>().cosmeticColor(new Color(0.8f, 0.8f, 0.8f, 0.4f));
                }
            }
        }
    }


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (isActive)
        {
            gameControllerScript.instance.MiddleMan(managerCalled, voidCalled, IDCalled);
        }
    }
}