using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Animator animator;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject item;
    [SerializeField] bool isActiveItem;
    [SerializeField] int numberOfItem;
    [SerializeField] string[] getDialogue, findDialogue;
    private int open;
    private bool isCollisionAllow = true;

    private void Start()
    {
        // PlayerPrefs.DeleteKey("Interact" + transform.name);
        CheckPoint.onCheckPointCross += OnCheckPointCross;
        open = PlayerPrefs.GetInt("Interact" + transform.name, 0);
        if (open == 0)
        {
            if (isActiveItem && item)
            {
                item.SetActive(false);
            }
            animator.Play("Close");
        }
        else
        {
            if (isActiveItem && item)
            {
                item.SetActive(true);
            }
            animator.Play("Open");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerHealth.isPlayerDye) return;

        if ((collision.gameObject.layer == 7) && (open == 0) && isCollisionAllow)
        {
            if (item != null)
            {
                AudioManager.instance.Play("Chest");
                animator.Play("Play");
                Instantiate(effect, transform.position, Quaternion.identity);
                for (int i = 0; i < numberOfItem; i++)
                {
                    if (isActiveItem)
                    {
                        item.SetActive(true);
                    }
                    else
                    {
                        Instantiate(item, new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1),
                        Random.Range(transform.position.y + 3, transform.position.y + 6), transform.position.z), Quaternion.identity);
                    }
                }
                dialogueManager.StartDialogue(getDialogue);
                open = 1;
            }
            else
            {
                if (SaveManager.instance.saveData.key > 0)
                {
                    AudioManager.instance.Play("Coin");
                    dialogueManager.StartDialogue(getDialogue);
                    SaveManager.instance.saveData.key--;
                    animator.Play("Play");
                    open = 1;
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

    private void OnDestroy()
    {
        CheckPoint.onCheckPointCross -= OnCheckPointCross;
    }

    private void EndDialogue()
    {
        dialogueManager.EndDialogue();
        isCollisionAllow = true;
    }

    private void OnCheckPointCross()
    {
        PlayerPrefs.SetInt("Interact" + transform.name, open);
    }
}