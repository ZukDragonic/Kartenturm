using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "SumObj", menuName = "Summoned Object")]
public class SOSumObj : ScriptableObject
{
    public int ID;

    public int damage;
    public int pierce;
    public SOCE_Parent effect;
    public int hitRadius;

    public Sprite image;
    public AnimatorController anim;
}
