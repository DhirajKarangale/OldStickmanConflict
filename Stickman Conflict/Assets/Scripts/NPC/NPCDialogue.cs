using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Transform player;

    [Header("Dialogues")]
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] bool isRandomDialogue, isOnlyOnce;
    [SerializeField] Dialogue[] dialogues;
    private Dialogue pickedDialogue;
    private static int dialoguePicker;
    public bool isDialogueAllow;

    private void Update()
    {
        if (isOnlyOnce) 
        {
            DialogueShow();
            return;
        }
        if (Random.value > 0.5f) DialogueShow();
    }

    private void DialogueShow()
    {
        if ((Mathf.Abs(player.transform.position.x - transform.position.x) < 10) && !dialogueManager.isEndDialogue)
        {
            if (!isDialogueAllow)
            {
                isDialogueAllow = true;
                dialogueManager.StartDialogue(DialogueToSent().sentences);
            }
        }
        else
        {
            if (isDialogueAllow)
            {
                dialogueManager.EndDialogue();
                isDialogueAllow = false;
                if (isOnlyOnce) enabled = false;
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
            if (dialoguePicker < (dialogues.Length - 1)) dialoguePicker++;
            else dialoguePicker = 0;
        }

        return pickedDialogue;
    }
}