using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class relayVoidScript : MonoBehaviour
{
    void cardsHasChanged()
    {
        StartCoroutine(waitOneFrame("cardsHasChanged"));
    }

    IEnumerator waitOneFrame(string message)
    {
        yield return null;
        transform.parent.SendMessage(message);
    }
}
