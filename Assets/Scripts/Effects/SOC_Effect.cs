using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOC_Effect : ScriptableObject
{
    public void ApplyEffect(sumObj_Base script)
    {
        if (script as sumObj_Standalone != null)
        {
            ApplyOSEffect(script as sumObj_Standalone);
        }
        else if (script as sumObj_Projectile != null)
        {
            ApplyOPEffect(script as sumObj_Projectile);
        }
        else if (script as baseTowerScript != null)
        {
            ApplyTEffect(script as baseTowerScript);
        }
        else if (script as SOEnemyDisplay)
        {
            ApplyEEffect(script as SOEnemyDisplay);
        }
        else
        {
            ApplyBEffect();
        }
    }
    public virtual void ApplyEEffect(SOEnemyDisplay script)
    {
        Debug.Log("DNE");
    }
    public virtual void ApplyTEffect(baseTowerScript script)
    {
        Debug.Log("DNE");
    }
    public virtual void ApplyOSEffect(sumObj_Standalone script)
    {
        Debug.Log("DNE");
    }
    public virtual void ApplyOPEffect(sumObj_Projectile script)
    {
        Debug.Log("DNE");
    }
    public virtual void ApplyBEffect()
    {
        Debug.Log("DNE");
    }
}
