using System.Threading;
using UnityEngine;

public abstract class targettingBase : ScriptableObject
{
    public abstract void EnterCheck(baseTowerScript script);
    public abstract void UpdateCheck(baseTowerScript script);
    public abstract void ExitCheck(baseTowerScript script);

    public abstract string TargettingCheck();

    public int FindIndexInArray(targettingBase[] allTargetting, targettingBase searched)
    {
        for(int i = 0; i < allTargetting.Length; i++)
        {
            if (allTargetting[i] == searched)
            {
                return i;
            }
        }
        return -1;
    }
}
