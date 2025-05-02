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

    public GameObject dialoguePrefab;

    private GameObject currentDialogue;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    private Image leftImage;
    private Image rightImage;

    public void CreateDialogue(int ID)
    {
        gameController = gameControllerScript.instance;

        sentences = new Queue<SingleDialogue>();

        currentDialogue = Instantiate(dialoguePrefab, gameController.allCanvas[1].transform);
        nameText = currentDialogue.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText = currentDialogue.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();

        leftImage = currentDialogue.transform.GetChild(2).GetChild(0).GetComponent<Image>();
        leftImage.gameObject.SetActive(false);
        rightImage = currentDialogue.transform.GetChild(2).GetChild(1).GetComponent<Image>();
        rightImage.gameObject.SetActive(false);

        foreach (SingleDialogue dialogue in FindWholeDialogue(ID).dialogue)
        {
            sentences.Enqueue(dialogue);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        curSDialogue = sentences.Dequeue();
        StopAllCoroutines();
        if (curSDialogue.textColor == Color.black)
        {
            dialogueText.color = new Color(0.098f, 0.098f, 0.098f);
        }
        else
        {
            dialogueText.color = curSDialogue.textColor;
        }
        nameText.text = curSDialogue.name;
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
        Destroy(currentDialogue);
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
