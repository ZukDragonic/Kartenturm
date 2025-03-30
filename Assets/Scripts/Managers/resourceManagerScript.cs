using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resourceManagerScript : MonoBehaviour
{
    public numberDisplayScript goldDisplay;
    public int playerGold;
    public numberDisplayScript healthDisplay;
    public int health;
    public TextMeshProUGUI waveDisplay;
    public int waveNum;

    public List<baseTowerScript> allTower;
    public List<SOEnemyDisplay> allEnemy;

    void Start()
    {
        goldDisplay = GameObject.Find("Gold Display").GetComponent<numberDisplayScript>();
        healthDisplay = GameObject.Find("Health Display").GetComponent<numberDisplayScript>();
        waveDisplay = GameObject.Find("Wave Display").GetComponent<TextMeshProUGUI>();
        allTower = new List<baseTowerScript>();
        changeGold(0);
        changeHealth(0);
    }

    public void changeGold(int value)
    {
        playerGold += value;
        goldDisplay.updateDisplay(playerGold);
    }
    public void updateWave(int value)
    {
        waveNum = value;
        waveDisplay.text = "Wave: " + waveNum.ToString();
    }

    public void changeHealth(int value)
    {
        health += value;
        healthDisplay.updateDisplay(health);
        if (health <= 0)
        {
            gameControllerScript.instance.FlagTrigger(30003);
        }
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
