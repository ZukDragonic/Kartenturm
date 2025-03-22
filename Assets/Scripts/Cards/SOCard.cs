using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardBase", menuName = "Card/Base")]
public class SOCard : ScriptableObject
{
    public int ID;
    public int shopGold;
    public int actionGold;
    public string archetype;
    public string cardName;
    [TextArea(5, 3)]
    public string description;
    public Sprite image;
}
