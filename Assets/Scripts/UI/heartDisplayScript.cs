using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class heartDisplayScript : MonoBehaviour
{
    //script should be attached to a GUI with a grid component
    public GameObject displayPrefab;
    //above is the default object used
    private List<GameObject> displayList;

    public void UpdateDisplay(int max, int current)
    {
        for (int i = 0; i < current; i++)
        {
            displayList[i].GetComponent<Image>().color = new Color (1, 1, 1, 1);
            //resets color to default
        }
        if (current < max)
        {
            //if conditional exist to make sure it doesnt inapproriately mark the wrong heart
            for (int i = current; i < max; i++)
            {
                displayList[i].GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 1);
                //makes it really dark
            }
        }
    }
}
