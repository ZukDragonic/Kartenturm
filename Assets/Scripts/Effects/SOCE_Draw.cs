using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectDraw", menuName = "Card/Effect/Draw")]
public class SOCE_Draw : SOC_Effect
{
    public int drawNum;
    public override void ApplyBEffect()
    {
        gameControllerScript.instance.deckManager.drawCard(drawNum);
    }
}
