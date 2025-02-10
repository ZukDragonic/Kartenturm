using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resourceManagerScript : MonoBehaviour
{
    public numberDisplayScript goldDisplay;
    public int gold;
    public int health;

    public List<GameObject> allTower;

    void Start()
    {
        goldDisplay = GameObject.Find("Gold Display").GetComponent<numberDisplayScript>();
        allTower = new List<GameObject>();
        changeGold(0);
    }

    public void changeGold(int value)
    {
        gold += value;
        goldDisplay.updateDisplay(gold);
    }

    public void changeHealth(int value)
    {
        health += value;
    }

    public void addTower(GameObject tower)
    {
        allTower.Add(tower);
    }
    public void removeTower(GameObject tower)
    {
        if (allTower.Contains(tower))
        {
            allTower.Remove(tower);
        }
    }
}
