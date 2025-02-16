using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCTEffect : ScriptableObject
{
    public void ApplyEffect(baseTowerScript tScript, projectileScript pScript)
    {
        if (pScript != null)
        {
            ApplyPEffect(pScript);
        }
        else
        {
            ApplyTEffect(tScript);
        }
    }
    public virtual void ApplyTEffect(baseTowerScript script)
    {
    }
    public virtual void ApplyPEffect(projectileScript script)
    {
    }
}
