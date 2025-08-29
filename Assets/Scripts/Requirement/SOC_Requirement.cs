using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOC_Requirement : ScriptableObject
{
    [HideInInspector]
    public bool requirement;
    public bool isFlipped;
    public bool ApplyRequirement(sumObj_Base script)
    {
        if (script as sumObj_Projectile != null)
        {
            return ApplyOPRequirement(script as sumObj_Projectile);
        }
        else if(script as sumObj_Minion != null)
        {
            return ApplyOMRequirement(script as sumObj_Minion);
        }
        else if (script as sumObj_Standalone != null)
        {
            return ApplyOSRequirement(script as sumObj_Standalone);
        }
        else if (script as baseTowerScript != null)
        {
            return ApplyTRequirement(script as baseTowerScript);
        }
        else if (script as SOEnemyDisplay)
        {
            return ApplyERequirement(script as SOEnemyDisplay);
        }
        else
        {
            return ApplyBRequirement();
        }
    }
    public virtual bool ApplyERequirement(SOEnemyDisplay script)
    {
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }
    public virtual bool ApplyTRequirement(baseTowerScript script)
    {
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }
    public virtual bool ApplyOMRequirement(sumObj_Minion script)
    {
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }
    public virtual bool ApplyOSRequirement(sumObj_Standalone script)
    {
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }
    public virtual bool ApplyOPRequirement(sumObj_Projectile script)
    {
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }
    public virtual bool ApplyBRequirement()
    {
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }

    public bool CheckFlipped()
    {
        if (isFlipped)
        {
            return !requirement;
        }
        else
        {
            return requirement;
        }
    }
}
