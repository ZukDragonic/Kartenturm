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
            int currentCC = gameObject.transform.childCount;
            if (cardInHandCount != currentCC || isCardDucked)
            {
                if (currentCC % 2 == 0)
                {
                    for (int i = 0; i < currentCC; i++)
                    {
                        if (i % 2 == 0)
                        {
                            gameObject.transform.GetChild(i).position = new Vector3(referenceVector3.x - (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 60,
                                referenceVector3.y - (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 10, 0);

                            gameObject.transform.GetChild(i).rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 5));
                        }
                        else
                        {
                            gameObject.transform.GetChild(i).position = new Vector3(referenceVector3.x + (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 60,
                                referenceVector3.y - (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 10, 0);

                            gameObject.transform.GetChild(i).rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * -5));
                        }
                    }
                }
                else
                {
                    for (int i = -1; i < currentCC - 1; i++)
                    {
                        if (i == -1)
                        {
                            gameObject.transform.GetChild(i + 1).position = new Vector3(referenceVector3.x - 5, referenceVector3.y - 5, 0);
                            gameObject.transform.GetChild(i + 1).rotation = Quaternion.Euler(Vector3.zero);
                        }
                        else
                        {
                            if (i % 2 == 0)
                            {
                                gameObject.transform.GetChild(i + 1).position = new Vector3(referenceVector3.x - (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 80,
                                    referenceVector3.y - (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 10, 0);

                                gameObject.transform.GetChild(i + 1).rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 5));
                            }
                            else
                            {
                                gameObject.transform.GetChild(i + 1).position = new Vector3(referenceVector3.x + (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 80,
                                    referenceVector3.y - (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 10, 0);

                                gameObject.transform.GetChild(i + 1).rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * -5));
                            }
                        }
                    }
                }
            }
            cardInHandCount = currentCC;
            isCardDucked = false;
        }
        else //if a card is being card
        {
            if (isCardDucked)
            {
                return;
            }

            if (cardInHandCount % 2 == 1)
            {
                gameObject.transform.GetChild(0).position = new Vector3(referenceVector3.x, referenceVector3.y - 60, 0);

                gameObject.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                if (cardInHandCount != 1)
                {
                    for (int i = 1; i < cardInHandCount; i++)
                    {
                        float xPos = referenceVector3.x;
                        if (i % 2 == 1)
                        {
                            xPos = xPos - (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 60;
                        }
                        else
                        {
                            xPos = xPos + (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 60;
                        }
                        gameObject.transform.GetChild(i).position = new Vector3(xPos, referenceVector3.y - 60, 0);
                        gameObject.transform.GetChild(i).rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                }
            }
            else
            {
                for (int i = 0; i < cardInHandCount; i++)
                {
                    float xPos = referenceVector3.x;
                    if (i % 2 == 1)
                    {
                        xPos = xPos - (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 60;
                    }
                    else
                    {
                        xPos = xPos + (Mathf.RoundToInt(i / 2 - 0.4f) + 1) * 60;
                    }
                    gameObject.transform.GetChild(i).position = new Vector3(xPos, referenceVector3.y - 60, 0);
                    gameObject.transform.GetChild(i).rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
            }
            isCardDucked = true;
        }
    }
}
