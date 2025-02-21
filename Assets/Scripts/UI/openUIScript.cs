using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openUIScript : MonoBehaviour
{
    public GameObject UI;

    public void OpenUI()
    {
        if (gameControllerScript.instance.state != GameState.PauseMenu)
        {
            Instantiate(UI, gameControllerScript.instance.topCanvas.transform);
            gameControllerScript.instance.UpdateGameState(GameState.PauseMenu);
        }
    }
}
