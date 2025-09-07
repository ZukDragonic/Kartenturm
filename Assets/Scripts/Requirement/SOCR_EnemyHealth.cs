using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardRequirementEnemyHealth", menuName = "Card/Requirement/Enemy Health")]
public class SOCR_EnemyHealth : SOC_Requirement
{
    public int targetInt;

    public override bool ApplyOMRequirement(sumObj_Minion script)
    {
        requirement = false;
        float enemyHealth = script.hitEnemy.health;

        if (enemyHealth >= targetInt)
        {
            requirement = true;
        }

        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyOSRequirement(sumObj_Standalone script)
    {
        requirement = false;
        float enemyHealth = script.hitObject.health;

        if (enemyHealth >= targetInt)
        {
            requirement = true;
        }

        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyOPRequirement(sumObj_Projectile script)
    {
        requirement = false;
        float enemyHealth = script.hitEnemy.health;

        if (enemyHealth >= targetInt)
        {
            requirement = true;
        }

        requirement = CheckFlipped();
        return requirement;
    }
}
