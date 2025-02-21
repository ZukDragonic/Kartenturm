using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeImageScript : MonoBehaviour
{
    public Sprite[] images;
    private int currentImage;
    private Image imageComp;

    public Managers managerChecked;
    public string varChecked;

    void Start()
    {
        imageComp = GetComponent<Image>();
        currentImage = checkVar();
        imageComp.sprite = images[currentImage];
    }
    public void nextImage()
    {
        currentImage++;
        if (currentImage > images.Length - 1)
        {
            currentImage -= images.Length;
        }
        imageComp.sprite = images[currentImage];
    }
    public void previousImage()
    {
        currentImage--;
        if (currentImage < 0)
        {
            currentImage += images.Length;
        }
        imageComp.sprite = images[currentImage];
    }
    public void selectImage(int index)
    {
        currentImage = index;
        imageComp.sprite = images[currentImage];
    }

    int checkVar()
    {
        switch (managerChecked)
        {
            case Managers.None:
                return 0;
            case Managers.Game:
                break;
            case Managers.Resource:
                break;
            case Managers.Time:
                switch (varChecked)
                {
                    case "Time":
                        return gameControllerScript.instance.timeManager.currentTime;
                    case "AutoStart":
                        if (gameControllerScript.instance.timeManager.isAutoStart)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    default:
                        break;
                }
                break;
            case Managers.Deck:
                break;
            case Managers.Wave:
                break;
            case Managers.Timeline:
                break;
            default:
                break;
        }
        return 0;
    }
}