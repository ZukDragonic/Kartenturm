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

    private bool isOverPlaceable;

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
        //for UI elements
        GameObject UIHit = eventData.pointerCurrentRaycast.gameObject;
        //for elements that are not UI
        GameObject GameHit = gameController.mainInput_Ray.hit;

        //checks if something is being hit
        /*if (UIHit == null)
        {
            hit = null;
            cosmetic.resetCard();
            isOverPlaceable = false;
            return;
        }*/
        if (gameController.state == GameState.Wave)
        {
            hit = GameHit;
            if (hit == null)
            {
                return;
            }
            if (GameHit.tag != "hi") //hi = ogSpotPrefab tag
            {
                isOverPlaceable = true;
            }
            else
            {
                isOverPlaceable = false;
            }
        }
        else if (gameController.state == GameState.ShopMenu)
        {
            hit = UIHit;
            if (eventData.pointerCurrentRaycast.gameObject.tag == "Slot")
            {
                //then checks if it is in a UI slot
                hit = eventData.pointerCurrentRaycast.gameObject;

                if (hit.GetComponent<placeableAreaScript>().isPlaceable)
                {
                    isOverPlaceable = true;
                    //cosmetic purposes
                    cosmetic.cosmeticColor(new Color(1.0f, 1.0f, 0, 0.5f));
                }
            }
            else
            {
                hit = null;
                cosmetic.resetCard();
                isOverPlaceable = false;
                return;
            }
        }
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().raycastTarget = true;

        if (isOverPlaceable)
        {
            if (gameController.state == GameState.Wave)
            {
                card.PlayCard();
                Destroy(gameObject);
            }
            if (gameController.state == GameState.ShopMenu)
            {
                //when it is  over a card slot
                hit.GetComponent<slotUGUI>().cardHasDropped(card);
                Destroy(gameObject);
            }
        }
        cosmetic.resetCard();
    }
}
