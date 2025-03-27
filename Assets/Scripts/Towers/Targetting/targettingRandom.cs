using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargettingRandom", menuName = "Targetting/Random")]
public class targettingRandom : targettingBase
{
    public override void EnterCheck(baseTowerScript script)
    {

    }
    public override void UpdateCheck(baseTowerScript script)
    {
        script.currentTarget = new GameObject();
        int ran = UnityEngine.Random.Range(0, 360);
        float ranRadian = ran / 180 * 3.14f;
        float xPos = Mathf.Cos(ranRadian);
        float yPos = Mathf.Sin(ranRadian);
        script.currentTarget.transform.position = new Vector3(xPos, yPos, 0);
        script.currentTarget.transform.position += script.transform.position;
    }
    public override void ExitCheck(baseTowerScript script)
    {
        int FoundIndex = FindIndexInArray(script.allTargetting, this);

        //target change can only ever be -1, 0, or 1
        FoundIndex += script.targetChange;
        if (FoundIndex >= script.allTargetting.Length)
        {
            FoundIndex = 0;
        }
        else if (FoundIndex < 0)
        {
            FoundIndex = script.allTargetting.Length;
        }
        script.currentTargetting = script.allTargetting[FoundIndex];
        script.targetChange = 0;
    }

    public override string TargettingCheck()
    {
        return "Random";
    }
}
