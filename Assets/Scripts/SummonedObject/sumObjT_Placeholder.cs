using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sumObjT_Placeholder : sumObj_Base
{
    public SOCTower baseCard;
    public List<SOCard> stackedCards;
    public SOCTower placeHolderCard;

    public int targetChange;

    public float range;
    //public int damage; is in sumObj_Base
    public int pierce;

    public int shotsPerAttack;
    public float secondsPerAttack;
    public float multiplier;

    public GameObject baseProjectile;


    /*void Start()
    {
        range = baseCard.range;
        damage = baseCard.damage;
        pierce = baseCard.pierce;
        shotsPerAttack = baseCard.shotsPerAttack;
        secondsPerAttack = baseCard.secondsPerAttack;
        effects = SOCE_Parent.CreateInstance(baseCard.effects);
        multiplier = baseCard.multiplier;

        stackedCards.Add(placeHolderCard);
        stackedCards.Add(placeHolderCard);
        stackedCards.Add(placeHolderCard);

        changeStats();
    }*/

    public void changeStats()
    {
        effects = SOCE_Parent.CreateInstance(baseCard.effects);
        damage = baseCard.damage;
        pierce = baseCard.pierce;
        range = baseCard.range;
        multiplier = baseCard.multiplier;
        secondsPerAttack = baseCard.secondsPerAttack;
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            float ogScale = transform.GetChild(i).localScale.x;
            transform.GetChild(i).localScale = Vector3.one;
        }

        //calc artifacts first
        foreach (SOCard slot in stackedCards)
        {
            if (slot as SOCArtifact != null)
            {
                SOCArtifact artifactSlot = slot as SOCArtifact;
                effects = SOCE_Parent.MergeInstances(effects, artifactSlot.addedEffects);
            }
        }
        //applies artifacts effect
        effects.OnSummonEffect(this);
        //calc tower bonus
        foreach (SOCard slot in stackedCards)
        {
            if (slot as SOCTower != null)
            {
                SOCTower towerSlot = slot as SOCTower;
                damage += (int)(towerSlot.damage / 2 * multiplier);
                pierce += (int)(towerSlot.pierce / 2 * multiplier);
                range += towerSlot.range / 2 * multiplier;
            }
        }
    }
}
