using UnityEngine;

public class WalkNPC : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Transform head;

    [Header("Attributes")]
    [SerializeField] float moveSpeed;
    [SerializeField] float leftDist, rightDist;
    private bool moveFront = true;

    [Header("Dialogue")]
    [SerializeField] bool isDialogueActive;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] string[] sentences1, sentences2, sentences3;
    private bool isDialogueAllow = true;

    private void Start()
    {
        animator.speed = 0.3f;
    }

    private void Update()
    {
        if (dialogueManager.endDialogue || !isDialogueActive)
        {
            Move();
            return;
        }

        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 10)
        {
            if (isDialogueAllow)
            {
                int randSent = Random.Range(1, 5);
                string[] sentences;
                if (randSent == 1) sentences = sentences1;
                else if (randSent == 2) sentences = sentences2;
                else sentences = sentences3;

                animator.Play("Idel");
                dialogueManager.StartDialogue(sentences);
                isDialogueAllow = false;
            }
        }
        else
        {
            if (!isDialogueAllow)
            {
                dialogueManager.EndDialogue();
                isDialogueAllow = true;
            }
            Move();
        }
    }

    private void Move()
    {
        // Move
        if (transform.position.x > rightDist) moveFront = false;     // Move Back
        else if (transform.position.x < leftDist) moveFront = true;  // Move Front

        if (moveFront)
        {
            moveFront = true;
            animator.Play("Walk");
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            head.localScale = new Vector3(0.67f, 0.7f, 1);
        }
        else
        {
            moveFront = false;
            animator.Play("Walk Back");
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            head.localScale = new Vector3(-0.67f, 0.7f, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(leftDist, -8, 0), 1);
        Gizmos.DrawSphere(new Vector3(rightDist, -8, 0), 1);
    }
}
