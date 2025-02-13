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
    public Image cardImage;
    public Image displayedImage;
    private Vector2 artworkProportions;

    public GameObject towerDP;
    public TextMeshProUGUI attackValueDisplay;
    public TextMeshProUGUI spaValueDisplay;
    public TextMeshProUGUI rangeValueDisplay;
    public TextMeshProUGUI descriptionTower;

    public GameObject spellDP;
    public TextMeshProUGUI descriptionSpell;

    public Sprite[] allCardSprite;
    //0 is back, 1 is empty, 2 is normal tower, 3 is aoe tower, 4 is spell, 5 is artifact

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
            cardImage.sprite = allCardSprite[1];
            displayedImage.gameObject.SetActive(false);
            return;
        }
        cardName.text = card.cardName;
        displayedImage.gameObject.SetActive(true);
        displayedImage.sprite = card.image;
        artworkProportions = card.image.rect.size;
        artworkProportions = artworkProportions.normalized;
        displayedImage.rectTransform.sizeDelta = artworkProportions * new Vector2(140, 120);

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
                cardImage.sprite = allCardSprite[3];
            }
            else
            {
                cardImage.sprite = allCardSprite[2];
            }
        }
        else
        {
            towerDP.SetActive(false);
            spellDP.SetActive(true);
            spellCard = card as SOCSpell;
            descriptionSpell.text = spellCard.description;

            cardImage.sprite = allCardSprite[4];
        }
        if(TryGetComponent<CCosmeticScript>(out CCosmeticScript script))
        {
            script.initiateOgSprite();
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
