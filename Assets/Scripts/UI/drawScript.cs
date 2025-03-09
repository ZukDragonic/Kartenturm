using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawScript : MonoBehaviour
{
    public List<SOCard> drawDeck;
    public GameObject TowerCardPrefab;
    public GameObject SpellCardPrefab;
    private GameObject playerHand;
    public gameControllerScript gameController;
    void Start()
    {
        gameController = gameControllerScript.instance;
        playerHand = GameObject.Find("playerHand").gameObject;
    }
    public void drawCard(int drawNum)
    {
        drawDeck = gameController.deckManager.drawDeck;
        for (int i = 0; i < drawNum; i++)
        {
            if (gameController.deckManager.drawDeck.Count > 0)
            {
                SOCard drawnCard = drawDeck[0];
                GameObject current;
                if (drawnCard as SOCTower != null)
                {
                    current = Instantiate(TowerCardPrefab, playerHand.transform);
                }
                else
                {
                    current = Instantiate(SpellCardPrefab, playerHand.transform);
                }
                current.GetComponent<SOCardDisplayScript>().card = drawnCard;

                gameController.deckManager.drawDeck.RemoveAt(0);
            }
        }
    }
}
