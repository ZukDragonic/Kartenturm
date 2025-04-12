using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOC_Requirement : ScriptableObject
{
    public bool requirement;
    public bool isFlipped;
    public bool ApplyRequirement(baseTowerScript tScript, sumObj_Base oScript)
    {
        if (oScript != null)
        {
            if (oScript as sumObj_Standalone != null)
            {
                return ApplyOSRequirement(oScript);
            }
            else
            {
                return ApplyOPRequirement(oScript);
            }
        }
        else if (tScript != null)
        {
            return ApplyTRequirement(tScript);
        }
        else
        {
            return ApplyBRequirement();
        }
    }
    public virtual bool ApplyTRequirement(baseTowerScript script)
    {
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }
    public virtual bool ApplyOSRequirement(sumObj_Base script)
    {
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }
    public virtual bool ApplyOPRequirement(sumObj_Base script)
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

    bool CheckFlipped()
    {
        if (isFlipped)
        {
            return !requirement;
        }
        else
        {
            return !requirement;
        }
    }
}
