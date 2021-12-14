using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] UnityEngine.UI.Text dialogueText;
    [SerializeField] AudioSource writeSound;
    private Queue<string> sentencesQue;
    private string sentence;

    public bool isEndDialogue = false;
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
        AudioManager.instance.Play("Button");
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

        sentence = sentencesQue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";


        foreach (char letter in sentence.ToCharArray())
        {
            if (!writeSound.isPlaying) writeSound.Play();
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        isDialogueStart = false;
        isEndDialogue = true;
        animator.Play("DialogueClose");
        Invoke("DeactiveEndDialogue", 10);
    }

    private void DeactiveEndDialogue()
    {
        isEndDialogue = false;
    }
}