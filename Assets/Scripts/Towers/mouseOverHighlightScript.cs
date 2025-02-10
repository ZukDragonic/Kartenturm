using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mouseOverHighlightScript : MonoBehaviour
{
    public Material outlineMat;
    public Material defaultMat;

    private gameControllerScript gameController;

    void Start()
    {
        gameController = gameControllerScript.instance;
    }

    void Update()
    {
        if (gameController.mainInput_Ray.hittingSomething && (gameController.mainInput_Ray.hit == gameObject))
        {
            if (gameObject.TryGetComponent<Image>(out Image image))
            {
                image.material = outlineMat;
            }
            if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.material = outlineMat;
            }
        }
        else
        {
            if (gameObject.TryGetComponent<Image>(out Image image))
            {
                image.material = defaultMat;
            }
            if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.material = defaultMat;
            }
        }
    }
}
