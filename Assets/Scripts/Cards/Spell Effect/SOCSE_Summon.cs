using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSpellEffectSummon", menuName = "Card/Spell/Effect/Summon")]
public class SOCSE_Summon : SOCSEffect
{
    public GameObject summonedPrefab;
    public override void ApplyEffect()
    {
        Instantiate(summonedPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }
}
