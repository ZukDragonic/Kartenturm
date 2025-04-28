using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTower", menuName = "Card/Tower")]
public class SOCTower : SOCard
{
    public int shotsPerAttack;

    public int damage;
    public float secondsPerAttack;
    public float range;
    public float multiplier;

    public Sprite projectileImage;

    public int pierce;

    public SOCE_Parent effects;

    public int slotNum;
    public targettingOption targetting;
    [SerializeField]
    public targettingBase[] allTargetting;

    public float selectRadius;
}
