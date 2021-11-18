using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Transform player;
    [SerializeField] WalkNPC walkNPC;

    [Header("Dialogues")]
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField]  string[] sentences1, sentences2, sentences3;
    private bool isDialogueAllow;

    private void Update()
    {
        if (!dialogueManager.isEndDialogue)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 10)
            {
                if (!isDialogueAllow)
                {
                    isDialogueAllow = true;
                    StartDialogue();
                }
            }
            else
            {
                if (isDialogueAllow)
                {
                    dialogueManager.EndDialogue();
                    isDialogueAllow = false;
                }
                if (walkNPC != null) walkNPC.Move();
            }
        }
        else
        {
            if (walkNPC != null) walkNPC.Move();
        }
    }

    private void StartDialogue()
    {
        int randSent = Random.Range(1, 5);
        string[] sentences;
        if (randSent == 1) sentences = sentences1;
        else if (randSent == 2) sentences = sentences2;
        else sentences = sentences3;

        dialogueManager.StartDialogue(sentences);
    }
}