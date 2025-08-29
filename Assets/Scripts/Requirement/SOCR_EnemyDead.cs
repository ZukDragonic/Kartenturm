using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardRequirementEnemyDead", menuName = "Card/Requirement/Enemy Dead")]
public class SOCR_EnemyDead : SOC_Requirement
{
    public override bool ApplyOMRequirement(sumObj_Minion script)
    {
        requirement = true;
        float enemyHealth = script.hitEnemy.health;

        if (enemyHealth > script.damage)
        {
            return false;
        }

        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyOSRequirement(sumObj_Standalone script)
    {
        requirement = true;

        float enemyHealth = script.hitObject.health;

        if (enemyHealth > script.damage)
        {
            return false;
        }

        requirement = CheckFlipped();
        return requirement;
    }
    public override bool ApplyOPRequirement(sumObj_Projectile script)
    {
        requirement = true;

        float enemyHealth = script.hitEnemy.health;

        if (enemyHealth > script.damage)
        {
            return false;
        }

        requirement = CheckFlipped();
        return requirement;
    }
}
