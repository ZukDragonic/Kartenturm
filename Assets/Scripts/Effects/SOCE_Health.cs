using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectHealth", menuName = "Card/Effect/Health")]
public class SOCE_Health : SOC_Effect
{
    public int health;
    public bool accountStat;
    public override void ApplyTEffect(baseTowerScript script)
    {
        int currentGold = health;
        int currentHealth = health;
        if (accountStat)
        {
            currentHealth += (int)(script.damage / 100);
        }
        gameControllerScript.instance.resourceManager.changeHealth(currentHealth);
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        int currentHealth = health;
        if (accountStat)
        {
            currentHealth += (int)(script.damage / 100);
        }
        gameControllerScript.instance.resourceManager.changeHealth(currentHealth);
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        int currentHealth = health;
        if (accountStat)
        {
            currentHealth += (int)(script.damage / 100);
        }
        gameControllerScript.instance.resourceManager.changeHealth(currentHealth);
    }
    public override void ApplyBEffect()
    {
        gameControllerScript.instance.resourceManager.changeGold(health);
    }
}