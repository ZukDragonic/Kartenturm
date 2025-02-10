using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotChildDisplayScript : MonoBehaviour
{
    public GameObject childPrefab;

    public int slotNum;

    public List<SOCard> stackedCards;

    public SOCard placeHolder;

    public List<GameObject> child;

    public void initializeStackedCards()
    {
        for (int i = 0; i < slotNum; i++)
        {
            stackedCards.Add(placeHolder);
        }
    }

    public void cardsHasChanged()
    {
        for (int i = 0; i < slotNum; i++)
        {
            if (stackedCards[i].cardName != "")
            {
                if (transform.childCount < i + 1)
                {
                    GameObject currentChild = Instantiate(childPrefab, transform);
                    currentChild.GetComponent<SOCardDisplayScript>().card = stackedCards[i];
                    transform.GetChild(i).GetComponent<placeableAreaScript>().isPlaceable = false;
                    child.Add(currentChild);
                }
                else if (child[i].GetComponent<SOCardDisplayScript>().card != stackedCards[i])
                {
                    Instantiate(childPrefab, transform);
                    transform.GetChild(i).GetComponent<placeableAreaScript>().isPlaceable = false;
                }
            }
        }

        if (transform.childCount < slotNum)
        {
            GameObject currentChild = Instantiate(childPrefab, transform);
            currentChild.GetComponent<placeableAreaScript>().isPlaceable = true;
            child.Add(currentChild);
        }
    }
}
