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

        foreach (TimelineObject item in playedAnim.boundObjects)
        {
            GameObject currentObject = Instantiate(item.boundObjects);

            TrackAsset currentTrack = playedAnim.timelineAnim.GetOutputTrack(item.boundTrack);

            director.SetGenericBinding(currentTrack, currentObject);
        }


        director.playableAsset = playedAnim.timelineAnim;
        director.Play(playedAnim.timelineAnim);
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
