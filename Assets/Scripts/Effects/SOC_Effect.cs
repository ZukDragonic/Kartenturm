using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOC_Effect : ScriptableObject
{
    public void ApplyEffect(baseTowerScript tScript, sumObj_Base oScript)
    {
        if (oScript != null)
        {
            if (oScript as sumObj_Standalone != null)
            {
                ApplyOSEffect(oScript);
            }
            else
            {
                ApplyOPEffect(oScript);
            }
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
        Debug.Log("DNE");
    }
    public virtual void ApplyOSEffect(sumObj_Base script)
    {
        Debug.Log("DNE");
    }
    public virtual void ApplyOPEffect(sumObj_Base script)
    {
        Debug.Log("DNE");
    }
    public virtual void ApplyBEffect()
    {
        Debug.Log("DNE");
    }
}
