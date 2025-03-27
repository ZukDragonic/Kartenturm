using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargettingNone", menuName = "Targetting/None")]
public class targettingNone : targettingBase
{
    public override void EnterCheck(baseTowerScript script)
    {
    }
    public override void UpdateCheck(baseTowerScript script)
    {
        script.currentTarget = null;
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
        return "None";
    }
}
