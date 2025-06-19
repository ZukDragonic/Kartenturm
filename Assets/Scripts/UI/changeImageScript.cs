using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeImageScript : MonoBehaviour
{
    public Sprite[] images;
    private int currentImage = 0;
    private Image imageComp;

    public Managers managerChecked;
    public string varChecked;
    public int flagChecked;

    void Awake()
    {
        imageComp = GetComponent<Image>();
        currentImage = checkVar();
        if (currentImage == -1)
        {
            currentImage = 0;
        }
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
                //1 means that it's active, 0 means inactive, so the inactive should be earlier in list
                if (gameControllerScript.instance.FlagInfo(flagChecked))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case Managers.Resource:
                break;
            case Managers.Time:
                switch (varChecked)
                {
                    case "Time":
                        return 2 * (gameControllerScript.instance.timeManager.prevTime - 1);
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