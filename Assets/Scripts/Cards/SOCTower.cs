using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTower", menuName = "Card/Tower")]
public class SOCTower : SOCard
{
    public int shotsPerAttack;

    public float attackValue;
    public float secondsPerAttack;
    public float range;
    public float multiplier;

    public Sprite projectileImage;

    public int projectilePierce;

    public SOCE_Parent effects;

    public int slotNum;
    public targettingOption targetting;

    public float selectRadius;
}
