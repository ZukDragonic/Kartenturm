using UnityEngine;

public abstract class targettingBase
{
    public abstract void EnterCheck(baseTowerScript script);
    public abstract void UpdateCheck(baseTowerScript script);
    public abstract void ExitCheck(baseTowerScript script);

    public abstract string TargettingCheck();
}
