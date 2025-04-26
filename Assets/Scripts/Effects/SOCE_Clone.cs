using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffectClone", menuName = "Card/Effect/Clone")]
public class SOCE_Clone : SOC_Effect
{
    public bool includeSlot;

    public int cloneInt;
    public bool addedToHand;
    public override void ApplyTEffect(baseTowerScript script)
    {
        List<SOCard> possibleClones = new List<SOCard>();
        SOCard newClone = Instantiate(script.baseCard);
        newClone.name = newClone.name.Replace("(Clone)", "");
        possibleClones.Add(newClone);

        if (includeSlot)
        {
            foreach (SOCard card in script.stackedCards)
            {
                if (card.cardName != "")
                {
                    newClone = Instantiate(card);
                    newClone.name = newClone.name.Replace("(Clone)", "");
                    possibleClones.Add(newClone);
                }
            }
        }

        for (int i = 0; i < cloneInt; i++)
        {
            newClone = Instantiate(possibleClones[Random.Range(0, possibleClones.Count)]);
            newClone.name = newClone.name.Replace("(Clone)", "");
            if (addedToHand)
            {
                gameControllerScript.instance.deckManager.AddCardToHand(newClone);
            }
            else
            {
                gameControllerScript.instance.deckManager.shuffleCardInDeck(newClone);
            }
        }
    }
    public override void ApplyBEffect()
    {
        if (gameControllerScript.instance.deckManager.drawDeck.Count <= 0)
        {
            return;
        }
        SOCard newClone = Instantiate(gameControllerScript.instance.deckManager.drawDeck[0]);
        newClone.name = newClone.name.Replace("(Clone)", "");

        for (int i = 0; i < cloneInt; i++)
        {
            if (addedToHand)
            {
                gameControllerScript.instance.deckManager.AddCardToHand(newClone);
            }
            else
            {
                gameControllerScript.instance.deckManager.shuffleCardInDeck(newClone);
            }
        }
    }
}
