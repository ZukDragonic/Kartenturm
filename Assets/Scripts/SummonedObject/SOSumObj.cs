using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SumObj", menuName = "Summoned Object")]
public class SOSumObj : ScriptableObject
{
    public int ID;

    public int damage;
    public int pierce;
    public SOCE_Parent effect;
    public float hitRadius;

    public float speed;

    public Sprite image;
    public AnimationClip animClip;

    public static SOSumObj ConvertEnemyToMinion(SOEnemy enemy, SOSumObj sumObj)
    {
        SOSumObj currentSumObj = ScriptableObject.CreateInstance<SOSumObj>();
        SOCE_Parent newEffect = Instantiate(sumObj.effect);
        newEffect.name = newEffect.name.Replace("(Clone)", "");
        currentSumObj.effect = newEffect;

        currentSumObj.damage = enemy.health;
        currentSumObj.hitRadius = enemy.hitRadius;
        currentSumObj.speed = enemy.speed;
        currentSumObj.image = enemy.image;
        currentSumObj.animClip = enemy.animClip;

        return currentSumObj;
    }
}
