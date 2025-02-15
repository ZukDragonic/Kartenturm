using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class cardHolderScript : MonoBehaviour
{
    public int cardInHandCount;
    private gameControllerScript gameController;

    private Vector3 referenceVector3;

    private bool isCardDucked;
    void Start()
    {
        gameController = gameControllerScript.instance;
        cardInHandCount = 0;
        referenceVector3 = transform.position;
    }
    void Update()
    {
        if (gameController.state == GameState.PauseMenu)
        {
            return;
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
        }
    }

    IEnumerator waitForTimeline(float time)
    {
        yield return new WaitForSeconds(time);
        isCardDucked = false;
    }

    void resetCardPosition()
    {
        int currentCC = gameObject.transform.childCount;
        if (cardInHandCount != currentCC || isCardDucked)
        {
            if (currentCC % 2 == 0)
            {
                for (int i = 0; i < currentCC; i++)
                {
                    if (i % 2 == 0)
                    {
                        gameObject.transform.GetChild(i).localPosition = new Vector3((Mathf.FloorToInt(i / 2) + 1) * -60,
                            (Mathf.FloorToInt(i / 2) + 1) * -10 * (Mathf.FloorToInt(i / 2) * 0.3f), 0);

                        gameObject.transform.GetChild(i).rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.FloorToInt(i / 2) + 1) * 5));
                    }
                    else
                    {
                        gameObject.transform.GetChild(i).localPosition = new Vector3((Mathf.FloorToInt(i / 2) + 1) * 60,
                            (Mathf.FloorToInt(i / 2) + 1) * -10 * (Mathf.FloorToInt(i / 2) * 0.3f), 0);

                        gameObject.transform.GetChild(i).localRotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.FloorToInt(i / 2) + 1) * -5));
                    }
                }
            }
            else
            {
                for (int i = -1; i < currentCC - 1; i++)
                {
                    if (i == -1)
                    {
                        gameObject.transform.GetChild(i + 1).localPosition = new Vector3(0, 5, 0);
                        gameObject.transform.GetChild(i + 1).localRotation = Quaternion.Euler(Vector3.zero);
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            gameObject.transform.GetChild(i + 1).localPosition = new Vector3((Mathf.FloorToInt(i / 2) + 1) * -80,
                                (Mathf.FloorToInt(i / 2) + 1) * -10 * (Mathf.FloorToInt(i / 2) * 0.3f), 0);

                            gameObject.transform.GetChild(i + 1).localRotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.FloorToInt(i / 2) + 1) * 5));
                        }
                        else
                        {
                            gameObject.transform.GetChild(i + 1).localPosition = new Vector3((Mathf.FloorToInt(i / 2) + 1) * 80,
                                (Mathf.FloorToInt(i / 2) + 1) * -10 * (Mathf.FloorToInt(i / 2) * 0.3f), 0);

                            gameObject.transform.GetChild(i + 1).localRotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.FloorToInt(i / 2) + 1) * -5));
                        }
                    }
                }
            }
        }
        cardInHandCount = currentCC;
    }
}
