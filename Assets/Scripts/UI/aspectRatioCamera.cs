using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aspectRatioCamera : MonoBehaviour
{
    public Vector2 targetAspect;

    private Vector2 res;

    //used to ensure all UI elements anchors appropriately to the new screen size
    private List<CanvasScaler> scalerList = new List<CanvasScaler>();

    public void StartAction()
    {
        scalerList = new List<CanvasScaler>();
        foreach (GameObject canvas in gameControllerScript.instance.allCanvas)
        {
            scalerList.Add(canvas.GetComponent<CanvasScaler>());
        }
        Adjust();
    }

    void Update()
    {
        if (res.x != Screen.width ||
            res.y != Screen.height)
        {
            Adjust();
            res = new Vector2(Screen.width, Screen.height);
        }
;
    }
    public void Adjust()
    {
        float desiredAspect = targetAspect.x / targetAspect.y;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / desiredAspect;

        Camera camera = GetComponent<Camera>();

        Rect rect = camera.rect;
        if (scaleHeight < 1.0f)
        {
            //letterbox
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            foreach (CanvasScaler scaler in scalerList)
            {
                scaler.matchWidthOrHeight = 0;
            }
        }
        else
        {
            //pillarbox
            float scaleWidth = 1.0f / scaleHeight;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            foreach (CanvasScaler scaler in scalerList)
            {
                scaler.matchWidthOrHeight = 1;
            }
        }
        camera.rect = rect;
    }
}
