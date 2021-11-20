using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Transform player;
    [SerializeField] WalkNPC walkNPC;

    [Header("Dialogues")]
    [SerializeField] bool isRandomDialogue;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Dialogue[] dialogues;
    private Dialogue pickedDialogue;
    private int[] rarity;
    static int dialoguePicker;
    private bool isDialogueAllow;

    private void Start()
    {
        rarity = new int[dialogues.Length];
        for (int i = 0; i < dialogues.Length; i++)
        {
            rarity[i] = dialogues[i].rarity;
        }
    }

    private void Update()
    {
        if ((Mathf.Abs(player.transform.position.x - transform.position.x) < 10) && !dialogueManager.isEndDialogue)
        {
            if (!isDialogueAllow)
            {
                isDialogueAllow = true;
                dialogueManager.StartDialogue(DialogueToSent(dialogues).sentences);
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

    private Dialogue DialogueToSent(Dialogue[] dialogues)
    {
        pickedDialogue = dialogues[dialoguePicker];
        if (isRandomDialogue)
        {
            dialoguePicker = Random.Range(0, 100);
            for (int i = 0; i < (rarity.Length - 1); i++)
            {
                if (dialoguePicker <= rarity[i])
                {
                    pickedDialogue = dialogues[i];
                    break;
                }
                else
                {
                    dialoguePicker -= rarity[i];
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