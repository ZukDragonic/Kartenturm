using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectSound", menuName = "Card/Effect/Sound")]
public class SOCTE_Sound : SOC_Effect
{
    public int soundID;
    public override void ApplyEEffect(SOEnemyDisplay script)
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
    public override void ApplyTEffect(baseTowerScript script)
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
    public override void ApplyOSEffect(sumObj_Standalone script)
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
    public override void ApplyOPEffect(sumObj_Projectile script)
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
    public override void ApplyBEffect()
    {
        gameControllerScript.instance.audioManager.PlaySound(soundID);
    }
}
