using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOC_Effect : ScriptableObject
{
    public void ApplyEffect(baseTowerScript tScript, sumObj_Base pScript)
    {
        if (pScript != null)
        {
            ApplyPEffect(pScript);
        }
        else if(tScript != null)
        {
            ApplyTEffect(tScript);
        }
        else
        {
            ApplyBEffect();
        }
    }
    public virtual void ApplyTEffect(baseTowerScript script)
    {
    }
    public virtual void ApplyPEffect(sumObj_Base script)
    {
    }
    public virtual void ApplyBEffect()
    {
    }
}
