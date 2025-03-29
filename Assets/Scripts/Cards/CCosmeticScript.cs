using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CCosmeticScript : MonoBehaviour
{
    private Sprite ogSprite;
    private List<Color> ogColor;

    private void Start()
    {
        initiateOgSprite();
    }

    public void initiateOgSprite()
    {
        ogSprite = GetComponent<Image>().sprite;
        ogColor = new List<Color>();
        foreach (TextMeshProUGUI text in transform.GetComponentsInChildren<TextMeshProUGUI>())
        {
            ogColor.Add(text.color);
        }
        foreach (Image image in transform.GetComponentsInChildren<Image>())
        {
            ogColor.Add(image.color);
        }
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
//        transform.GetComponent<Image>().color = color;
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
        TextMeshProUGUI[] textComp = transform.GetComponentsInChildren<TextMeshProUGUI>();
        Image[] imageComp = transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < textComp.Length; i++)
        {
            textComp[i].color = ogColor[i];
        }

        int imageStart = textComp.Length;
        for (int i = imageStart; i < imageStart + imageComp.Length; i++)
        {
            imageComp[i - imageStart].color = ogColor[i];
        }

        transform.GetComponent<Image>().sprite = ogSprite;
        cosmeticSize(1);
    }
}
