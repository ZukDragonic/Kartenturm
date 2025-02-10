using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CIHTowerScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private gameControllerScript gameController;
    private CCosmeticScript cosmetic;

    public SOCTower card;
    
    private Sprite ogSprite;

    private bool isOverPlaceable;
    LayerMask placeableArea;
    private Ray wPRay;

    private GameObject hit;

    void Start()
    {
        placeableArea = LayerMask.GetMask("Placeable");
        ogSprite = GetComponent<Image>().sprite;
        gameController = gameControllerScript.instance;
        cosmetic = GetComponent<CCosmeticScript>();
        card = gameObject.GetComponent<SOCardDisplayScript>().card as SOCTower;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        wPRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //checks if something is being hit
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            hit = null;
            cosmetic.resetCard();
            isOverPlaceable = false;
            return;
        }
        if (gameController.stateReference == "Wave")
        {
            if (gameController.mainInput_Ray.hit.tag == "Tower Placeable")
            {
                //first checks if raycast hits a placeable area
                hit = gameController.mainInput_Ray.hit;

                if (hit.GetComponent<placeableAreaScript>().isPlaceable)
                {
                    isOverPlaceable = true;
                    //cosmetic purposes
                    foreach (TextMeshProUGUI text in transform.GetComponentsInChildren<TextMeshProUGUI>())
                    {
                        text.enabled = false;
                    }
                    foreach (Image image in transform.GetComponentsInChildren<Image>())
                    {
                        if (image.gameObject.name != "Image")
                        {
                            image.enabled = false;
                        }
                    }
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
        else if (gameController.stateReference == "GameMenu")
        {

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
            //create a spot that this checks for when dropped. if the spot is there, it stay
            if (gameController.stateReference == "Wave")
            {
                //when it is  over a placeable area
                GameObject placedCard = Instantiate(card.tower, hit.transform.position,
                    Quaternion.identity, hit.transform);
                placedCard.GetComponent<baseTowerScript>().baseCard = card;

                //place anywhere change
                hit.GetComponent<placeableAreaScript>().isPlaceable = false;
                hit.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(gameObject);
            }
            if (gameController.stateReference == "GameMenu")
            {
                //when it is  over a card slot
                Destroy(gameObject);
            }
        }
        cosmetic.resetCard();
        isOverPlaceable = false;
    }
}