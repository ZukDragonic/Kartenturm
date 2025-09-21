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

    private int ogSnapForce;
    public int snapForce;

    //velocity fix since velocity doesnt seem to work
    private float currentPos;
    private float prevPos;
    private float positionDiff;

    void Start()
    {
        //change scrollbar into a button where it shows the current item
        ogSnapForce = snapForce;
        //fixes problem where scroll spawns in middle
        StartCoroutine(DoLater());
    }

    IEnumerator DoLater()
    {
        yield return null;
        contentWindow.localPosition = new Vector3(
    0.5f * (contentWindow.rect.width),
    contentWindow.localPosition.y,
    contentWindow.localPosition.z);
    }

    void Update()
    {
        //total space divided by reference gets the # of content and current #
        //change x to y if vertical
        int currentItem = Mathf.RoundToInt(contentWindow.localPosition.x / (elementReference.rect.width + hLayoutGroup.spacing));

        prevPos = currentPos;
        currentPos = scrollRect.horizontalNormalizedPosition;
        positionDiff = currentPos - prevPos;

        //change x to y if vertical
        //should be scrollRect.velocity.magnitude but it don't work
        if (positionDiff <= 0.001f && canSnap)
        {
            snapForce = ogSnapForce;
            contentWindow.localPosition = new Vector3(
                Mathf.MoveTowards(contentWindow.localPosition.x, currentItem * (elementReference.rect.width + hLayoutGroup.spacing), snapForce),
               contentWindow.localPosition.y,
               contentWindow.localPosition.z);
            prevPos = currentPos;
            positionDiff = 0;
            if (contentWindow.localPosition.x == ((currentItem * (elementReference.rect.width + hLayoutGroup.spacing))))
            {
                canSnap = false;
            }
        }
        if (positionDiff != 0)
        {
            snapForce = 0;
            canSnap = true;
        }
    }
}
