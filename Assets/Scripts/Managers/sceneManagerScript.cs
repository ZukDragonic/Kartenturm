using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerScript : MonoBehaviour
{
    public GameObject transitionPrefab;
    private GameObject currentTransition;
    private int endID;

    public SceneNameAndID[] allScenes;

    public void LoadNewScene(int ID, int EndID)
    {
        if (currentTransition == null)
        {
            endID = EndID;
            currentTransition = Instantiate(transitionPrefab);
            DontDestroyOnLoad(currentTransition);
            StartCoroutine(LoadingScene(ID));
        }
    }

    IEnumerator LoadingScene(int ID)
    {
        Animator transitionAnim = currentTransition.GetComponent<Animator>();

        yield return new WaitUntil(() => transitionAnim.GetCurrentAnimatorStateInfo(0).IsName("FadeMiddle"));


        string FoundScene = FindScene(ID);
        SceneManager.LoadScene(FoundScene);

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == FoundScene);

        gameControllerScript.instance.InitiateManagerStart();
        gameControllerScript.instance.FlagTrigger(endID);
        transitionAnim.Play("FadeEnd", 0);

        yield return new WaitUntil(() => transitionAnim.GetCurrentAnimatorStateInfo(0).IsName("FadeDestroy"));

        Destroy(currentTransition);
        currentTransition = null;
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