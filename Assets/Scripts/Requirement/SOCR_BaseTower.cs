using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardRequirementBaseT", menuName = "Card/Requirement/Base Tower")]
public class SOCR_BaseTower : SOC_Requirement
{
    public SOCTower checkedTower;
    //if base tower is same as checked tower, standard returns true
    public override bool ApplyTRequirement(baseTowerScript script)
    {
        requirement = true;
        if (script.baseCard != checkedTower)
        {
            requirement = false;
        }
        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyOSRequirement(sumObj_Base script)
    {
        Debug.Log("DNE");
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyOPRequirement(sumObj_Base script)
    {
        requirement = true;
        if ((script as sumObj_Projectile).parentTower.baseCard != checkedTower)
        {
            requirement = false;
        }
        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyBRequirement()
    {
        Debug.Log("DNE");
        requirement = true;
        requirement = CheckFlipped();
        return requirement;
    }
}
