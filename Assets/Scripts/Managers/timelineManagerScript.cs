using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class timelineManagerScript : MonoBehaviour
{
    public PlayableDirector director;
    public WholeAnimation[] allAnim;

    private WholeAnimation playedAnim;

    private void Start()
    {
        director = GetComponentInParent<PlayableDirector>();
    }

    public void PlayAnim(int ID)
    {
        playedAnim = FindTimeline(ID);

        director.playableAsset = playedAnim.timelineAnim;

        for(int i = 0; i < playedAnim.boundObjects.Length;i++)
        {
            GameObject currentObject = null;
            if (playedAnim.boundObjects[i].findObjectInScene)
            {
                currentObject = GameObject.Find(playedAnim.boundObjects[i].objectInSceneName);
                if (currentObject == null)
                {
                    Debug.Log("You entered the wrong name. Find a proper item to bind.");
                }
            }
            else
            {
                currentObject = Instantiate(playedAnim.boundObjects[i].boundObjects);
            }

            foreach (var track in director.playableAsset.outputs)
            {
                director.SetGenericBinding(track.sourceObject, currentObject);
            }
        }

        director.Play();
    }

    IEnumerator WaitUntilStop()
    {
        yield return new WaitForSeconds((float)playedAnim.timelineAnim.duration);
    }

    WholeAnimation FindTimeline(int ID)
    {
        foreach (WholeAnimation anim in allAnim)
        {
            if (anim.ID == ID)
            {
                return anim;
            }
        }
        return null;
    }
}
