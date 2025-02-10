using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOEnemy : ScriptableObject
{
    public int ID;

    public string Enemyname;
    [TextArea(5, 3)]
    public string description;
    public Sprite image;

    public float speed;
    public float health;
    public int damageToHP;

    public float hitRadius;

    public int goldDrop;
}
