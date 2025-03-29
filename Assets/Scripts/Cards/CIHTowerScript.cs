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
    private placeTowerScript placeTower;
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
        if (gameController.state == GameState.Wave || gameController.state == GameState.BetweenWave)
        {
            gameController.UpdateGameState(GameState.Placing);
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        wPRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = gameController.mainInput_Ray.hit;
        //checks if something is being hit
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            hit = null;
            cosmetic.resetCard();
            isOverPlaceable = false;
            isTowerShown = false;
            Destroy(showTower);
            return;
        }
        if (gameController.state == GameState.Placing)
        {
            if (hit == null)
            {
                return;
            }
            if (!isTowerShown)
            {
                //cosmetic purposes
                cosmetic.cosmeticColor(new Color(0, 0, 0, 0));
                showTower = Instantiate(showTowerPrefab, hit.transform);
                showTower.GetComponent<tCosmeticScript>().showTower(card);
                showTower.GetComponent<tCosmeticScript>().rangeDisplayUpdate(true, card.range);
                placeTower = showTower.GetComponent<placeTowerScript>();
                isTowerShown = true;
            }
            else
            {
                showTower.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (placeTower.isPlaceable && gameController.resourceManager.playerGold >= card.actionGold)
            {
                isOverPlaceable = true;
                showTower.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0, 0.5f);
            }
            else
            {
                isOverPlaceable = false;
                showTower.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.1f, 0.1f, 0.5f);
            }
        }
        else if (gameController.state == GameState.TowerMenu || gameController.state == GameState.ShopMenu)
        {

            if (eventData.pointerCurrentRaycast.gameObject.tag == "Slot")
            {
                //then checks if it is in a UI slot
                hit = eventData.pointerCurrentRaycast.gameObject;

                if (hit.GetComponent<placeableAreaScript>().isPlaceable)
                {
                    if (gameController.resourceManager.playerGold >= card.actionGold)
                    {
                        isOverPlaceable = true;
                        //cosmetic purposes
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

        if (isOverPlaceable)
        {
            //create a spot that this checks for when dropped. if the spot is there, it stay
            if (gameController.state == GameState.Placing)
            {
                //when it is  over a placeable area
                GameObject placedCard = Instantiate(baseTower, showTower.transform.position,
                    Quaternion.identity, showTower.transform.parent);
                placedCard.transform.position = new Vector3(showTower.transform.position.x, showTower.transform.position.y, 0);
                placedCard.GetComponent<baseTowerScript>().baseCard = card;

                gameController.UpdateGameState(GameState.Previous);
                gameController.resourceManager.changeGold(card.actionGold * -1);

                //change for limited placements
                /*hit.GetComponent<placeableAreaScript>().isPlaceable = false;
                hit.GetComponent<CircleCollider2D>().enabled = false;*/
                if (showTower != null)
                {
                    isTowerShown = false;
                    Destroy(showTower);
                    showTower = null;
                }
                Destroy(gameObject);
            }
            if (gameController.state == GameState.TowerMenu || gameController.state == GameState.ShopMenu)
            {
                //when it is  over a card slot
                hit.GetComponent<slotUGUI>().cardHasDropped(card);
                if (gameController.state == GameState.TowerMenu)
                {
                    gameController.resourceManager.changeGold(card.actionGold * -1);
                }
                Destroy(gameObject);
            }
        }
        if (showTower != null)
        {
            isTowerShown = false;
            Destroy(showTower);
            showTower = null;
        }
        if (gameController.state == GameState.Placing)
        {
            gameController.UpdateGameState(GameState.Previous);
        }
        cosmetic.resetCard();
        isOverPlaceable = false;
    }
}