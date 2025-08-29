using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOC_Effect : ScriptableObject
{
    [TextArea(3, 2)]
    public string effectDescription;

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
        else if (script as sumObjT_Placeholder != null)
        {
            ApplyPHEffect(script as sumObjT_Placeholder);
        }
        else if (script as baseTowerScript != null)
        {
            ApplyTEffect(script as baseTowerScript);
        }
        else if (script as SOEnemyDisplay != null)
        {
            ApplyEEffect(script as SOEnemyDisplay);
        }
        else if (script as sumObj_Minion != null)
        {
            ApplyOMEffect(script as sumObj_Minion);
        }
        else
        {
            ApplyBEffect();
        }
    }
    public virtual void ApplyEEffect(SOEnemyDisplay script)
    {
        //enemy effect
        Debug.Log("DNE");
    }
    public virtual void ApplyPHEffect(sumObjT_Placeholder script)
    {
        //placeholder calculator effect
        Debug.Log("DNE");
    }
    public virtual void ApplyTEffect(baseTowerScript script)
    {
        //object tower effect
        Debug.Log("DNE");
    }
    public virtual void ApplyOMEffect(sumObj_Minion script)
    {
        //object summon effect
        Debug.Log("DNE");
    }
    public virtual void ApplyOSEffect(sumObj_Standalone script)
    {
        //object standalone effect
        Debug.Log("DNE");
    }
    public virtual void ApplyOPEffect(sumObj_Projectile script)
    {
        //object projcetile effect
        Debug.Log("DNE");
    }
    public virtual void ApplyBEffect()
    {
        //bsae effect
        Debug.Log("DNE");
    }

    public string EffectDescription()
    {
        if (effectDescription == "")
        {
            return null;
        }
        return effectDescription;
    }
}
