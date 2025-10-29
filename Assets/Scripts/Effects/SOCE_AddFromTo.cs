using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectAddfromto", menuName = "Card/Effect/AddFromTo")]
public class SOCE_AddFromTo : SOC_Effect
{
    public int AddNum;
    public GameObject deckObject;
    public Location startLocation;
    public Location endLocation;
    public override void ApplyBEffect()
    {
        gameControllerScript.instance.deckManager.SearchCard(AddNum, startLocation, endLocation);
        //Instantiate(deckObject, gameControllerScript.instance.allCanvas[1].transform);
    }
}
