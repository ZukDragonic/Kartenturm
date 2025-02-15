using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeImageScript : MonoBehaviour
{
    public Sprite[] images;
    private int currentImage;
    private Image imageComp;

    void Start()
    {
        imageComp = GetComponent<Image>();
        currentImage = 0;
        imageComp.sprite = images[0];
    }
    public void nextImage()
    {
        currentImage++;
        if (currentImage > images.Length -1)
        {
            currentImage -= images.Length;
        }
        imageComp.sprite = images[currentImage];
    }
    public void selectImage(int index)
    {
        currentImage = index;
        imageComp.sprite = images[currentImage];
    }
}
