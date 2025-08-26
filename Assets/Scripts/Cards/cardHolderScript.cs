using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardHolderScript : MonoBehaviour
{
    public int cardInHandCount;
    private gameControllerScript gameController;

    private Vector3 referenceVector3;

    private bool isCardDucked;
    private bool isCardMinimized;

    public float maximizeTime;
    private float minimizeTimer;
    private IEnumerator minimizeCoroutine;

    void Start()
    {
        gameController = gameControllerScript.instance;
        cardInHandCount = 0;
        referenceVector3 = transform.position;
        StartCoroutine(DoItLater());
    }

    IEnumerator DoItLater()
    {
        yield return null;
        gameController.timelineManager.PlayAnim(13001);
        resetCardPosition();
    }
    void Update()
    {
        if (gameController.state == GameState.PauseMenu)
        {
            return;
        }

        if (gameController.isCardHovered)
        {
            minimizeTimer = 0;
            if (isCardMinimized)
            {
                isCardMinimized = false;
                //makes cards bigger
                gameController.timelineManager.PlayAnim(13002);
            }

            if (!gameController.isCardDragged)
            {
                if (isCardDucked)
                {
                    gameController.timelineManager.PlayAnim(13001);
                    isCardDucked = false;
                }
                resetCardPosition();
            }
            else //if a card is being dragged
            {
                if (isCardDucked)
                {
                    return;
                }
                gameController.timelineManager.PlayAnim(13000);
                isCardDucked = true;
                resetCardPosition();
            }
        }
        else
        {
            if (isCardDucked)
            {
                gameController.timelineManager.PlayAnim(13001);
                isCardDucked = false;
                resetCardPosition();
            }
            else
            {
                if (!isCardMinimized)
                {
                    if (minimizeCoroutine == null)
                    {
                        minimizeCoroutine = MinimizeCards();
                        StartCoroutine(minimizeCoroutine);
                    }
                }
            }
        }
    }

    IEnumerator MinimizeCards()
    {
        while (minimizeTimer <= maximizeTime)
        {
            minimizeTimer += Time.deltaTime;
            yield return null;
        }
        minimizeCoroutine = null;
        isCardMinimized = true;
        gameController.timelineManager.PlayAnim(13003);
    }

    /* //remove in future build?
    IEnumerator waitForTimeline(float time)
    {
        yield return new WaitForSeconds(time);
        isCardDucked = false;
    }*/

    void resetCardPosition()
    {
        int currentCC = gameObject.transform.childCount;
        if (cardInHandCount != currentCC || isCardDucked)
        {
            if (currentCC % 2 == 0)
            {
                for (int i = 0; i < currentCC; i++)
                {
                    Vector3 lPos;
                    Quaternion lRot;
                    Transform currentChild = gameObject.transform.GetChild(i);
                    if (i % 2 == 0)
                    {
                        lPos = new Vector3((Mathf.FloorToInt(i / 2) + 1) * -60,
                            (Mathf.FloorToInt(i / 2) + 1) * -10 * (Mathf.FloorToInt(i / 2) * 0.3f), 0);
                        lRot = Quaternion.Euler(new Vector3(0, 0, (Mathf.FloorToInt(i / 2) + 1) * 5));

                        currentChild.localPosition = lPos;
                        currentChild.rotation = lRot;
                    }
                    else
                    {
                        lPos = new Vector3((Mathf.FloorToInt(i / 2) + 1) * 60,
                            (Mathf.FloorToInt(i / 2) + 1) * -10 * (Mathf.FloorToInt(i / 2) * 0.3f), 0);
                        lRot = Quaternion.Euler(new Vector3(0, 0, (Mathf.FloorToInt(i / 2) + 1) * -5));

                        currentChild.localPosition = lPos;
                        currentChild.localRotation = lRot;
                    }
                    cardInHandScript childCIH = currentChild.GetComponent<cardInHandScript>();
                    childCIH.ogPos = lPos;
                    childCIH.ogRotation = lRot;
                }
            }
            else
            {
                for (int i = -1; i < currentCC - 1; i++)
                {
                    Vector3 lPos;
                    Quaternion lRot;
                    Transform currentChild = gameObject.transform.GetChild(i + 1);
                    if (i == -1)
                    {
                        lPos = new Vector3(0, 5, 0);
                        lRot = Quaternion.Euler(Vector3.zero);
                        currentChild.localPosition = lPos;
                        currentChild.localRotation = lRot;
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            lPos = new Vector3((Mathf.FloorToInt(i / 2) + 1) * -80,
                                (Mathf.FloorToInt(i / 2) + 1) * -10 * (Mathf.FloorToInt(i / 2) * 0.3f), 0);
                            lRot = Quaternion.Euler(new Vector3(0, 0, (Mathf.FloorToInt(i / 2) + 1) * 5));

                            currentChild.localPosition = lPos;
                            currentChild.localRotation = lRot;
                        }
                        else
                        {
                            lPos = new Vector3((Mathf.FloorToInt(i / 2) + 1) * 80,
                                (Mathf.FloorToInt(i / 2) + 1) * -10 * (Mathf.FloorToInt(i / 2) * 0.3f), 0);
                            lRot = Quaternion.Euler(new Vector3(0, 0, (Mathf.FloorToInt(i / 2) + 1) * -5));

                            currentChild.localPosition = lPos;
                            currentChild.localRotation = lRot;
                        }
                    }
                    cardInHandScript childCIH = currentChild.GetComponent<cardInHandScript>();
                    childCIH.ogPos = lPos;
                    childCIH.ogRotation = lRot;
                }
            }
        }
        cardInHandCount = currentCC;
    }
}
