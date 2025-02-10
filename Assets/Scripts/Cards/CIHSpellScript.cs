using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CIHSpellScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //* Everything is indented once here
    private gameControllerScript gameController;
    private CCosmeticScript cosmetic;
    public SOCSpell card;
    public GameObject ogSpotPrefab;
    private GameObject currentSpotObject;

    private bool isOverSA;

    private GameObject hit;

    void Start()
    {
        gameController = gameControllerScript.instance;
        cosmetic = gameObject.GetComponent<CCosmeticScript>();

        card = gameObject.GetComponent<SOCardDisplayScript>().card as SOCSpell;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().raycastTarget = false;
        Vector3 xzPos = gameObject.transform.position;
        //currentSpotObject = Instantiate(ogSpotPrefab, new Vector3(xzPos.x, ogSpotPrefab.transform.position.y, xzPos.z), Quaternion.identity);
        cosmetic.cosmeticColor(new Color(1.0f, 1.0f, 0, 0.5f));
        cosmetic.cosmeticSize(0.5f);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        hit = eventData.pointerCurrentRaycast.gameObject;
        if (hit != null)
        {
            if (hit.tag != "hi") //hi = ogSpotPrefab tag
            {
                isOverSA = true;
            }
            else
            {
                isOverSA = false;
            }
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (isOverSA)
        {
            card.PlayCard();
            Destroy(gameObject);
        }
    }
}
