using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] string[] getDialogue, findDialogue;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Animator animator;
    [SerializeField] GameObject effect, openLight;
    [SerializeField] GameObject item;
    [SerializeField] int numberOfItem;
    private int open;
    private bool isCollisionAllow = true;

    private void Start()
    {
       // PlayerPrefs.DeleteKey("Interact" + transform.name);
        open = PlayerPrefs.GetInt("Interact" + transform.name, 0);
        if (open == 0) animator.Play("Close");
        else if (open == 1) animator.Play("Open");
        if (openLight != null) openLight.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7) && (open == 0) && isCollisionAllow)
        {
            //  CancelInvoke("EndDialogue");
            if (item != null)
            {
                animator.Play("Play");
                Instantiate(effect, transform.position, Quaternion.identity);
                for (int i = 0; i < numberOfItem; i++)
                {
                    Instantiate(item, new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1), Random.Range(transform.position.y + 3, transform.position.y + 6), transform.position.z), Quaternion.identity);
                }
                dialogueManager.StartDialogue(getDialogue);
                openLight.SetActive(true);
                open = 1;
                PlayerPrefs.SetInt("Interact" + transform.name, open);
            }
            else
            {
                if (SaveManager.instance.saveData.key > 0)
                {
                    dialogueManager.StartDialogue(getDialogue);
                    SaveManager.instance.saveData.key--;
                    animator.Play("Play");
                    open = 1;
                    PlayerPrefs.SetInt("Interact" + transform.name, open);
                }
                else
                {
                    dialogueManager.StartDialogue(findDialogue);
                }
            }
            Invoke("EndDialogue", 2);
            isCollisionAllow = false;
        }
    }

    private void EndDialogue()
    {
        dialogueManager.EndDialogue();
        isCollisionAllow = true;
    }
}