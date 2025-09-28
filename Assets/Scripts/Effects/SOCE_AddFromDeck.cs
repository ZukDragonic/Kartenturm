using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectAddfromdeck", menuName = "Card/Effect/Add From Deck")]
public class SOCE_AddFromDeck : SOC_Effect
{
    public int AddNum;
    public GameObject deckObject;
    public override void ApplyBEffect()
    {
        gameControllerScript.instance.deckManager.SearchCard(gameControllerScript.instance.deckManager.drawDeck.Count);
        //Instantiate(deckObject, gameControllerScript.instance.allCanvas[1].transform);
    }
}
