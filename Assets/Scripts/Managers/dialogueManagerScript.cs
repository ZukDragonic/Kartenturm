using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogueManagerScript : MonoBehaviour
{
    public gameControllerScript gameController;

    public WholeDialogue[] dialogueList;

    private Queue<SingleDialogue> sentences;
    private SingleDialogue curSDialogue;
    private WholeDialogue curWDialogue;

    public GameObject dialoguePrefab;


    public Sprite[] dialogueBackground;
    private GameObject currentDialogue;
    private TextMeshProUGUI nameTextL;
    private TextMeshProUGUI nameTextR;
    private TextMeshProUGUI dialogueText;
    private Image leftImage;
    private Image rightImage;

    public void CreateDialogue(int ID)
    {
        gameController = gameControllerScript.instance;
        gameController.timeManager.pauseTime();

        sentences = new Queue<SingleDialogue>();

        currentDialogue = Instantiate(dialoguePrefab, gameController.allCanvas[3].transform);
        nameTextL = currentDialogue.transform.Find("Background").Find("Name L").gameObject.GetComponent<TextMeshProUGUI>();
        nameTextR = currentDialogue.transform.Find("Background").Find("Name R").gameObject.GetComponent<TextMeshProUGUI>();
        dialogueText = currentDialogue.transform.Find("Background").Find("Dialogue").gameObject.GetComponent<TextMeshProUGUI>();

        leftImage = currentDialogue.transform.Find("Images").Find("Left Image").gameObject.GetComponent<Image>();
        leftImage.gameObject.SetActive(false);
        rightImage = currentDialogue.transform.Find("Images").Find("Right Image").gameObject.GetComponent<Image>();
        rightImage.gameObject.SetActive(false);


        curWDialogue = FindWholeDialogue(ID);
        foreach (SingleDialogue dialogue in curWDialogue.dialogue)
        {
            sentences.Enqueue(dialogue);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        StopAllCoroutines();

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        curSDialogue = sentences.Dequeue();
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);

        if (curSDialogue.textColor == Color.black)
        {
            nameTextL.color = curSDialogue.textColor;
            nameTextR.color = curSDialogue.textColor;
            dialogueText.color = new Color(0.098f, 0.098f, 0.098f);
        }
        else
        {
            nameTextL.color = curSDialogue.textColor;
            nameTextR.color = curSDialogue.textColor;
            dialogueText.color = curSDialogue.textColor;
        }

        if (curSDialogue.imageSide == ImageSide.Left)
        {
            leftImage.gameObject.SetActive(true);
            dialogueText.transform.parent.GetComponent<Image>().sprite = dialogueBackground[0];
            nameTextL.text = curSDialogue.name;
            nameTextR.text = "";
            leftImage.sprite = curSDialogue.image;
            rightImage.sprite = null;
            if (curSDialogue.image == null)
            {
                leftImage.gameObject.SetActive(false);
            }
        }
        else if (curSDialogue.imageSide == ImageSide.Right)
        {
            rightImage.gameObject.SetActive(true);
            dialogueText.transform.parent.GetComponent<Image>().sprite = dialogueBackground[1];
            nameTextR.text = curSDialogue.name;
            nameTextL.text = "";
            rightImage.sprite = curSDialogue.image;
            leftImage.sprite = null;
            if (curSDialogue.image == null)
            {
                rightImage.gameObject.SetActive(false);
            }
        }
        StartCoroutine(TypeSentece(curSDialogue.text));
    }

    IEnumerator TypeSentece(string sentence)
    {
        dialogueText.text = "";
        foreach (char character in sentence.ToCharArray())
        {
            dialogueText.text += character;
            yield return null;
        }
    }

    void EndDialogue()
    {
        gameController.FlagTrigger(curWDialogue.ID);
        Destroy(currentDialogue);
        gameController.timeManager.resetTime();
    }

    WholeDialogue FindWholeDialogue(int ID)
    {
        foreach (WholeDialogue WD in dialogueList)
        {
            if (WD.ID == ID)
            {
                return WD;
            }
        }
        return null;
    }
}
