using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resourceManagerScript : MonoBehaviour
{
    public numberDisplayScript goldDisplay;
    public int playerGold;
    public int health;

    public List<baseTowerScript> allTower;
    public List<SOEnemyDisplay> allEnemy;

    void Start()
    {
        goldDisplay = GameObject.Find("Gold Display").GetComponent<numberDisplayScript>();
        allTower = new List<baseTowerScript>();
        changeGold(0);
    }

    public void changeGold(int value)
    {
        playerGold += value;
        goldDisplay.updateDisplay(playerGold);
    }

    public void changeHealth(int value)
    {
        health += value;
    }

    public void addTower(baseTowerScript tower)
    {
        allTower.Add(tower);
    }
    public void removeTower(baseTowerScript tower)
    {
        if (allTower.Contains(tower))
        {
            allTower.Remove(tower);
        }
    }
    public void removeEnemy(SOEnemyDisplay enemy)
    {
        if (allEnemy.Contains(enemy))
        {
            allEnemy.Remove(enemy);
        }
    }
}
