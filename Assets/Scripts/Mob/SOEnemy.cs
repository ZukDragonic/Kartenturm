using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Enemy")]
public class SOEnemy : ScriptableObject
{
    public int ID;

    public string Enemyname;
    [TextArea(5, 3)]
    public string description;
    public string undiscoveredDes;
    public Sprite image;
    public AnimationClip animClip;

    public float speed;
    public int health;
    public int damageToHP;
    public int spawnValue;

    public float hitRadius;

    public int goldDrop;

    public SOCE_Parent effects;
    public EnemyAttack[] attacks;

    public bool targettable;
}
