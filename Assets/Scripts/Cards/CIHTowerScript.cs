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

    public GameObject baseTower;
    public GameObject showTowerPrefab;
    private GameObject showTower;
    private bool isTowerShown;

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
                        image.enabled = false;
                    }
                    if (!isTowerShown)
                    {
                        showTower = Instantiate(showTowerPrefab, hit.transform.position, Quaternion.identity, hit.transform);
                        showTower.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0, 0.5f);
                        showTower.GetComponent<tCosmeticScript>().showTower(card);
                        showTower.GetComponent<tCosmeticScript>().rangeActivity(true, card.range);
                        transform.position = Camera.main.WorldToScreenPoint(hit.transform.position);
                        isTowerShown = true;
                    }
                }
            }
            else
            {
                hit = null;
                cosmetic.resetCard();
                isOverPlaceable = false;
                isTowerShown = false;
                Destroy(showTower);
                showTower = null;
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
                Destroy(showTower);
                showTower = null;
                return;
            }
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().raycastTarget = true;

        if (showTower != null)
        {
            isTowerShown = false;
            Destroy(showTower);
            showTower = null;
        }

        if (isOverPlaceable)
        {
            //create a spot that this checks for when dropped. if the spot is there, it stay
            if (gameController.stateReference == "Wave")
            {
                //when it is  over a placeable area
                GameObject placedCard = Instantiate(baseTower, hit.transform.position,
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