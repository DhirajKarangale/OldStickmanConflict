using UnityEngine;

public class KeyMachine : MonoBehaviour
{
    [SerializeField] Dialogue getDalogue,findDialogue;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if(SaveManager.instance.saveData.key > 0)
            {
                dialogueManager.StartDialogue(getDalogue.sentences);
                SaveManager.instance.saveData.key--;
                animator.Play("Start");
            }
            else
            {
                dialogueManager.StartDialogue(findDialogue.sentences);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            dialogueManager.EndDialogue();
        }
    }
}