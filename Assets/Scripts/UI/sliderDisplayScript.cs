using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderDisplayScript : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider = transform.GetChild(0).GetComponent<Slider>();
    }
    public void updateDisplay(float current, float max)
    {
        slider = transform.GetChild(0).GetComponent<Slider>();

        slider.value = current / max;
    }
}
