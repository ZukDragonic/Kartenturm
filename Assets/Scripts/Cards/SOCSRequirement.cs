using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCSRequirement : ScriptableObject
{
    public virtual bool ApplyRequirement()
    {
        return true;
    }
}
