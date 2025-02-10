using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class numberDisplayScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void updateDisplay(int value)
    {
        text.text = value.ToString();
    }
}
