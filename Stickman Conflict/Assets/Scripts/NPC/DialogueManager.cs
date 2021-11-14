using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text dialogueText;
    private Queue<string> sentencesQue;
  
    public bool endDialogue;
    private bool isDialogueStart;

    private void Start()
    {
        sentencesQue = new Queue<string>();
    }

    private void Update()
    {
        if (isDialogueStart && (Input.GetMouseButtonDown(0)))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(string[] sentences)
    {
        isDialogueStart = true;
        animator.Play("DialogueOpen");
        sentencesQue.Clear();
        foreach (string sentence in sentences)
        {
            sentencesQue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentencesQue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentencesQue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        isDialogueStart = false;
        endDialogue = true;
        animator.Play("DialogueClose");
        Invoke("DeactiveEndDialogue", 10);
    }

    private void DeactiveEndDialogue()
    {
        endDialogue = false;
    }
}