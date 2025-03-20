using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeTowerScript : MonoBehaviour
{
    public bool isPlaceable;

    private List<GameObject> touchedObjects;

    private void Start()
    {
        isPlaceable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BlockPlaceable")
        {
            touchedObjects.Add(collision.gameObject);
            isPlaceable = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "BlockPlaceable")
        {
            touchedObjects.Remove(collision.gameObject);
            if (touchedObjects.Count == 0)
            {
                isPlaceable = true;
            }
        }
    }
}
