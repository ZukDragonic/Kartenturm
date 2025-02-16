using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTower", menuName = "Card/Tower")]
public class SOCTower : SOCard
{
    public bool isAreaAttack;
    public bool isScatter;

    public int shotsPerAttack;
    public float spreadOfAttack;

    public float attackValue;
    public float secondsPerAttack;
    public float range;
    public float multiplier;

    public float projectileSpeed;
    public float projectileDuration;
    public bool isProjectileBounce;
    public int projectileBounceNum;
    public int projectilePierce;

    public SOCTE_Parent Effects;

    public int slotNum;
    public targettingOption targetting;
}
