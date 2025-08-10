using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerScript : MonoBehaviour
{
    public GameObject transitionPrefab;
    private GameObject currentTransition;

    public SceneNameAndID[] allScenes;

    public int restartScene;

    public GameObject levelSelectorPrefab;

    public void LoadNewScene(int ID)
    {
        if (currentTransition == null)
        {
            if (ID == 23000)
            {
                LoadNewScene(restartScene);
                return;
            }
            currentTransition = Instantiate(transitionPrefab);
            DontDestroyOnLoad(currentTransition);
            StartCoroutine(LoadingScene(ID));
        }
    }

    IEnumerator LoadingScene(int ID)
    {
        //delayed in case another flag is called at the same frame
        yield return null;

        Animator transitionAnim = currentTransition.GetComponent<Animator>();

        yield return new WaitUntil(() => transitionAnim.GetCurrentAnimatorStateInfo(0).IsName("FadeMiddle"));


        string FoundScene = FindScene(ID);
        SceneManager.LoadScene(FoundScene);

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == FoundScene);

        restartScene = ID;
        gameControllerScript.instance.FlagTrigger(ID);
        transitionAnim.Play("FadeEnd", 0);

        yield return new WaitUntil(() => transitionAnim.GetCurrentAnimatorStateInfo(0).IsName("FadeDestroy"));

        Destroy(currentTransition);
        currentTransition = null;
    }

    public void InstantiateLevelMenu(int ID)
    {
        GameObject currentLevelMenu = Instantiate(levelSelectorPrefab, gameControllerScript.instance.allCanvas[1].transform);

        for (int i = 1; i < currentLevelMenu.transform.childCount; i++)
        {
            foreach (M_middleManCallScript script in currentLevelMenu.transform.GetChild(i).GetComponents<M_middleManCallScript>())
            {
                if (script.IDCalled == 23000)
                {
                    script.IDCalled = ID;
                }

                //Apply restriction if not the story selector
                if (i != 1)
                {
                    script.IDCheckedForActive = 21000 + ID - 23012;
                }


                /*(must be else or else if or else the prev statement will force the next to be true
                else if (script.IDCheckedForActive == 21000)
                {
                    script.IDCheckedForActive = 21000 + ID - 23012;
                }*/
            }
        }
    }

    string FindScene(int ID)
    {
        foreach (SceneNameAndID current in allScenes)
        {
            if (current.ID == ID)
            {
                return current.sceneName;
            }
        }
        return SceneManager.GetActiveScene().name;
    }
}