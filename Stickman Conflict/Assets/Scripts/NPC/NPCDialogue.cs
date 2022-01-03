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
    private bool isDialogueAllow = true, isActivateAllowDialogue;
    private float currTime = 0;

    private void Update()
    {
        if (dialogues.Length == 1)
        {
            ShowDialogue();
            return;
        }
        if (isActivateAllowDialogue) AllowDialogue();
        if (Random.value > 0.5f) ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < dist)
        {
            if (isDialogueAllow)
            {
                isDialogueAllow = false;
                dialogueManager.StartDialogue(DialogueToSent().sentences);
            }
        }
        else
        {
            if (!isDialogueAllow)
            {
                dialogueManager.EndDialogue();
                isActivateAllowDialogue = true;
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
        if (!isDialogueAllow)
        {
            if (currTime >= 20)
            {
                currTime = 0;
                isDialogueAllow = true;
            }
            else
            {
                currTime += Time.deltaTime;
            }
        }
        else
        {
            currTime = 0;
            isActivateAllowDialogue = false;
        }
    }
}