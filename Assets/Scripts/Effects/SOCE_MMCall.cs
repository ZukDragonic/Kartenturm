using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "CardEffectMmcall", menuName = "Card/Effect/MidMan Call")]
public class SOCE_MMCall : SOC_Effect
{
    public Managers managerCalled;
    public string voidCalled;
    public int IDCalled;

    public override void ApplyBEffect()
    {
        gameControllerScript.instance.MiddleMan(managerCalled, voidCalled, IDCalled);
    }
}
