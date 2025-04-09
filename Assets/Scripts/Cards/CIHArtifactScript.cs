using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CIHArtifactScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private gameControllerScript gameController;
    private CCosmeticScript cosmetic;
    public SOCArtifact card;

    private bool isOverPlaceable;

    private GameObject hit;

    void Start()
    {
        gameController = gameControllerScript.instance;
        cosmetic = gameObject.GetComponent<CCosmeticScript>();

        card = gameObject.GetComponent<SOCardDisplayScript>().card as SOCArtifact;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {

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
        if (gameController.state == GameState.Placing)
        {
            //nah don't do shit. it doesn't do shit during wave
        }
        else if (gameController.state == GameState.TowerMenu)
        {
            hit = UIHit;
            if (hit == null)
            {
                return;
            }
            if (hit.gameObject.tag == "Slot")
            {
                if (hit.GetComponent<placeableAreaScript>().isPlaceable)
                {
                    if (gameController.resourceManager.playerGold >= card.actionGold)
                    {
                        isOverPlaceable = true;
                        cosmetic.cosmeticColor(new Color(1.0f, 1.0f, 0, 0.5f));
                    }
                    else
                    {
                        isOverPlaceable = false;
                        cosmetic.cosmeticColor(new Color(1.0f, 0.1f, 0.1f, 0.5f));
                    }
                }
            }
            else
            {
                hit = null;
                isOverPlaceable = false;
                cosmetic.resetCard();
            }
        }
        else if (gameController.state == GameState.ShopMenu)
        {
            //for selling ig
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
            if (gameController.state == GameState.TowerMenu)
            {
                hit.GetComponent<slotUGUI>().cardHasDropped(card);
                gameController.resourceManager.changeGold(card.actionGold * -1);
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
