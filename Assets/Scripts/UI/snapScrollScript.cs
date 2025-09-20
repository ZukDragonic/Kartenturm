using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class snapScrollScript : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentWindow;
    public RectTransform elementReference;

    //change to verticalLayoutGroup if vertical
    public HorizontalLayoutGroup hLayoutGroup;

    private bool canSnap;
    private bool isSnapping;

    //velocity fix since velocity doesnt seem to work
    private float currentPos;
    private float prevPos;
    private float positionDiff;

    void Start()
    {
        scrollRect.horizontalScrollbar.value = 0;
    }

    void Update()
    {
        //total space divided by reference gets the # of content and current #
        int currentItem = Mathf.RoundToInt(contentWindow.localPosition.x / (elementReference.rect.width + hLayoutGroup.spacing));
        //change x to y if vertical

        prevPos = currentPos;
        currentPos = scrollRect.horizontalNormalizedPosition;
        positionDiff = currentPos - prevPos;

        Debug.Log(positionDiff);

        //change x to y if vertical
        //should be scrollRect.velocity.magnitude but it don't work
        if (positionDiff == 0 && canSnap)
        {
            contentWindow.localPosition = new Vector3(
                Mathf.MoveTowards(contentWindow.localPosition.x, currentItem * (elementReference.rect.width + hLayoutGroup.spacing), 1f),
               contentWindow.localPosition.y,
               contentWindow.localPosition.z);
            prevPos = currentPos;
            positionDiff = 0;
            if(contentWindow.localPosition.x ==  ((currentItem * (elementReference.rect.width + hLayoutGroup.spacing))))
            {
                canSnap = false;
            }
        }
        if (positionDiff != 0)
        {
            canSnap = true;
        }
    }
}
