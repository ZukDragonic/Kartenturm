using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSpellEffectDraw", menuName = "Card/Spell/Effect/Draw")]
public class SOCSEDraw : SOCSEffect
{
    public int drawNum;
    public override void ApplyEffect()
    {
        gameControllerScript.instance.deckManager.drawCard(drawNum);
    }
}
