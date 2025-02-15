using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class timelineManagerScript : MonoBehaviour
{
    public PlayableDirector director;
    public TimelineAnimation[] allAnim;

    private void Start()
    {
        director = GetComponentInParent<PlayableDirector>();
    }

    public void PlayAnim(int ID)
    {
        foreach (TimelineAnimation anim in allAnim)
        {
            if (anim.ID == ID)
            {
                director.Play(anim.timelineAnim);
                //director.playableAsset = anim.timelineAnim;
            }
        }
    }
}
