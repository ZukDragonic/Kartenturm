using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CCosmeticScript : MonoBehaviour
{
    private Sprite ogSprite;

    public void initiateOgSprite()
    {
        ogSprite = GetComponent<Image>().sprite;
    }
    public void cosmeticColor(Color color)
    {
        foreach (TextMeshProUGUI text in transform.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.color = color;
        }
        foreach (Image image in transform.GetComponentsInChildren<Image>())
        {
            image.color = color;
        }
        transform.GetComponent<Image>().color = color;
    }

    public void cosmeticSize(float size)
    {
        foreach (RectTransform transform in transform.GetComponentsInChildren<RectTransform>())
        {
            transform.localScale = new Vector3(size,size,size);
        }
    }

    public void resetSprite()
    {
        GetComponent<Image>().sprite = ogSprite;
        transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true; ;
    }

    public void resetCard()
    {
        foreach (TextMeshProUGUI text in transform.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.enabled = true;
            text.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }
        foreach (Image image in transform.GetComponentsInChildren<Image>())
        {
            image.enabled = true;
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        transform.GetComponent<Image>().sprite = ogSprite;
        cosmeticSize(1);
    }
}
