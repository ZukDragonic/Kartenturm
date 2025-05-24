using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerScript : MonoBehaviour
{
    public GameObject transitionPrefab;
    private GameObject currentTransition;

    public SceneNameAndID[] allScenes;

    public void LoadNewScene(int ID)
    {
        if (currentTransition == null)
        {
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

        gameControllerScript.instance.StartAction();
        gameControllerScript.instance.FlagTrigger(ID);
        transitionAnim.Play("FadeEnd", 0);

        yield return new WaitUntil(() => transitionAnim.GetCurrentAnimatorStateInfo(0).IsName("FadeDestroy"));

        Destroy(currentTransition);
        currentTransition = null;
    }

    string FindScene(int ID)
    {
        if (ID == 23000)
        {
            return SceneManager.GetActiveScene().name;
        }
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