using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Transform player;
    [SerializeField] DialogueManager dialogueManager;

    [Header("Dialogues")]
    [SerializeField] bool isRandomDialogue;
    [SerializeField] float dist;
    [SerializeField] Dialogue[] dialogues;
    private Dialogue pickedDialogue;
    private int dialoguePicker = 0;
    private bool isDialogueAllow = true;

    private void Update()
    {
        if (dialogues.Length == 1)
        {
            DialogueShow();
            return;
        }
        if (Random.value > 0.5f) DialogueShow();
    }

    private void DialogueShow()
    {
        if ((Mathf.Abs(player.transform.position.x - transform.position.x) < dist) && !dialogueManager.isEndDialogue)
        {
            if (isDialogueAllow)
            {
                dialogueManager.StartDialogue(DialogueToSent().sentences);
                isDialogueAllow = false;
            }
        }
        else
        {
            if (!isDialogueAllow)
            {
                dialogueManager.EndDialogue();
                Invoke("AllowDialogue", 20);
                if (dialogues.Length == 1) enabled = false;
            }
        }
    }

    private Dialogue DialogueToSent()
    {
        pickedDialogue = dialogues[dialoguePicker];
        if (isRandomDialogue)
        {
            dialoguePicker = Random.Range(0, 100);
            for (int i = 0; i < (dialogues.Length - 1); i++)
            {
                if (dialoguePicker <= dialogues[i].rarity)
                {
                    pickedDialogue = dialogues[i];
                    break;
                }
                else
                {
                    dialoguePicker -= dialogues[i].rarity;
                }
            }
            dialoguePicker = 0;
        }
        else
        {
            dialoguePicker = (dialoguePicker + 1) % dialogues.Length;
        }

        return pickedDialogue;
    }

    private void AllowDialogue()
    {
        isDialogueAllow = true;
    }
}