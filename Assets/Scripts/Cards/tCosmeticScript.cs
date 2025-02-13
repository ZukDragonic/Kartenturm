using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tCosmeticScript : MonoBehaviour
{
    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    public GameObject rangeIndicatorPrefab;
    private GameObject rIObject;
    public Material hoverMat;
    public Material clickMat;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }
    public void showTower(SOCTower card)
    {
        GetComponent<SpriteRenderer>().sprite = card.image;
    }

    public void ResetMat()
    {
        spriteRenderer.material = defaultMat;
    }
    public void HoverMat()
    {
        spriteRenderer.material = hoverMat;
    }
    public void ClickMat()
    {
        spriteRenderer.material = clickMat;
    }
    public void flipSprite(bool flip)
    {
        spriteRenderer.flipX = flip;
    }
    public void rangeActivity(bool isShown, float range)
    {
        if (isShown)
        {
            rIObject = Instantiate(rangeIndicatorPrefab, transform.position, Quaternion.identity, transform);
            rIObject.GetComponent<Transform>().localScale = Vector3.one * range / 5 / 3.125f;
        }
        else
        {
            Destroy(rIObject);
        }
    }
}
