using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Animator animator;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject item;
    [SerializeField] int numberOfItem;
    private string[] getItemDialogue = new string[1];
    private int open;

    private void Start()
    {
        getItemDialogue[0] = "Congratulations You Found " + item.name + "!!!";
        CheckPoint.onCheckPointCross += OnCheckPointCross;
        open = PlayerPrefs.GetInt("Chest" + transform.name, 0);

        if (open == 0)
        {
            if ((numberOfItem == 0) && item)
            {
                item.SetActive(false);
            }
            animator.Play("Close");
        }
        else
        {
            if ((numberOfItem == 0) && item)
            {
                item.SetActive(true);
            }
            animator.Play("Open");
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerHealth.isPlayerDye && (open == 1))
        {
            this.enabled = false;
            return;
        }

        if ((collision.gameObject.layer == 7) && (open == 0))
        {
            AudioManager.instance.Play("Chest");
            animator.Play("Play");
            Instantiate(effect, transform.position, Quaternion.identity);

            if (numberOfItem == 0)
            {
                item.SetActive(true);
            }
            else
            {
                for (int i = 0; i < numberOfItem; i++)
                {
                    Instantiate(item, new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1),
                    Random.Range(transform.position.y + 3, transform.position.y + 6), transform.position.z), Quaternion.identity);
                }
            }

            dialogueManager.StartDialogue(getItemDialogue);
            open = 1;

            Invoke("EndDialogue", 2);
        }
    }

    private void OnDestroy()
    {
        CheckPoint.onCheckPointCross -= OnCheckPointCross;
    }

    private void EndDialogue()
    {
        dialogueManager.EndDialogue();
    }

    private void OnCheckPointCross()
    {
        PlayerPrefs.SetInt("Chest" + transform.name, open);
    }
}