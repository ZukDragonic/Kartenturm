using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeTowerScript : MonoBehaviour
{
    public bool isPlaceable;

    private List<GameObject> touchedObjects;

    public GameObject myBlock;

    private void Start()
    {
        isPlaceable = true;
        touchedObjects = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == myBlock)
        {
            return;
        }
        
        if (collision.tag == "Block Placeable")
        {
            touchedObjects.Add(collision.gameObject);
            isPlaceable = false;
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == myBlock)
        {
            return;
        }
        
        if (collision.tag == "Block Placeable")
        {
            touchedObjects.Remove(collision.gameObject);
            if (touchedObjects.Count == 0)
            {
                isPlaceable = true;
            }
        }
    }
}
