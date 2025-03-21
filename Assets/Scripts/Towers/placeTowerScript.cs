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

    private void Update()
    {
        transform.position += Vector3.right * 0.01f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.tag == "Block Placeable")
        {
            Debug.Log("hi");
            touchedObjects.Add(collision.gameObject);
            isPlaceable = false;
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        Debug.Log("a");
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("b");
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("c");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("d");
        if (collision.tag == "Block Placeable")
        {
            Debug.Log("e");
            touchedObjects.Remove(collision.gameObject);
            if (touchedObjects.Count == 0)
            {
                isPlaceable = true;
            }
        }
    }
}
