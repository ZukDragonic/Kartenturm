using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CIHSpellScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
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
        if (gameController.state == GameState.Wave || gameController.state == GameState.BetweenWave)
        {
            currentSpotObject = Instantiate(ogSpotPrefab, Vector3.zero, Quaternion.identity, gameController.allCanvas[2].transform);
            currentSpotObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(transform.position.x, -50, 0);
            gameController.UpdateGameState(GameState.Placing);
        }
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
            hit = GameHit;
            if (hit == null || UIHit == null)
            {
                return;
            }
            if (UIHit.tag != "Card" &&
                 gameController.resourceManager.playerGold >= card.actionGold)
            {
                cosmetic.cosmeticColor(new Color(1.0f, 1.0f, 0, 0.5f));
                cosmetic.cosmeticSize(0.5f);
                isOverPlaceable = true;
            }
            else
            {
                cosmetic.cosmeticColor(new Color(1.0f, 0.1f, 0.1f, 0.5f));
                cosmetic.cosmeticSize(1f);
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
            if (gameController.state == GameState.Placing)
            {
                card.PlayCard();
                gameController.UpdateGameState(GameState.Previous);
                gameController.resourceManager.changeGold(card.actionGold * -1);
                Destroy(currentSpotObject);
                Destroy(gameObject);
            }
            if (gameController.state == GameState.ShopMenu)
            {
                //when it is  over a card slot
                hit.GetComponent<slotUGUI>().cardHasDropped(card);
                Destroy(gameObject);
            }
        }
        if (gameController.state == GameState.Placing)
        {
            Destroy(currentSpotObject);
            gameController.UpdateGameState(GameState.Previous);
        }
        cosmetic.resetCard();
    }
}
