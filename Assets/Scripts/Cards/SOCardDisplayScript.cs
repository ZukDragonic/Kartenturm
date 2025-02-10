using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Mathematics;
using System.Linq.Expressions;

public class SOCardDisplayScript : MonoBehaviour
{
    public SOCard card;
    private SOCTower towerCard;
    private SOCSpell spellCard;

    public TextMeshProUGUI cardName;
    public Image artwork;

    public GameObject towerDP;
    public TextMeshProUGUI attackValueDisplay;
    public TextMeshProUGUI spaValueDisplay;
    public TextMeshProUGUI rangeValueDisplay;
    public TextMeshProUGUI descriptionTower;

    public GameObject spellDP;
    public TextMeshProUGUI descriptionSpell;

    public Sprite areaAttackSprite;
    public Sprite towerCardSprite;
    public Sprite spellCardSprite;

    void Start()
    {
        displayCard(card);
    }

    public void displayCard(SOCard Card)
    {
        card = Card;
        if (checkNull())
        {
            cardName.text = "";
            artwork.gameObject.SetActive(false);
            return;
        }
        cardName.text = card.cardName;
        artwork.gameObject.SetActive(true);
        artwork.sprite = card.image;

        if (card as SOCTower != null)
        {
            spellDP.SetActive(false);
            towerDP.SetActive(true);
            towerCard = card as SOCTower;
            attackValueDisplay.text = towerCard.attackValue.ToString();
            spaValueDisplay.text = towerCard.secondsPerAttack.ToString();
            rangeValueDisplay.text = towerCard.range.ToString();
            descriptionTower.text = card.description;
            if (towerCard.isAreaAttack)
            {
                GetComponent<Image>().sprite = areaAttackSprite;
            }

            GetComponent<Image>().sprite = towerCardSprite;
        }
        else
        {
            towerDP.SetActive(false);
            spellDP.SetActive(true);
            spellCard = card as SOCSpell;
            descriptionSpell.text = spellCard.description;

            GetComponent<Image>().sprite = spellCardSprite;
        }
    }

    bool checkNull()
    {
        if (card.cardName == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
