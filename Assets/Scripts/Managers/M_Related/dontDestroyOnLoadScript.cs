using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyOnLoadScript : MonoBehaviour
{
    public static List<GameObject> current;
    // Start is called before the first frame update
    void Start()
    {
        bool containObject = false;
        if (current == null)
        {
            current = new List<GameObject>();
        }
        foreach (GameObject copy in current)
        {
            if (copy.name == gameObject.name)
            {
                containObject = true;
            }
        }
        if (!containObject)
        {
            current.Add(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
