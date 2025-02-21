using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class closeScript : MonoBehaviour, IPointerDownHandler
{
    public GameObject destroyObject;
    void Start()
    {
        destroyObject = transform.parent.gameObject;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Destroy(destroyObject);
        gameControllerScript.instance.UpdateGameState(GameState.Wave);
    }
}
