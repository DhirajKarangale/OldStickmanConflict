using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] PlayerHealth player;
    public Animator animator;
    public Rigidbody2D rigidBody;

    [Header("Attributes")]
    [SerializeField] float moveSpeed;
    [SerializeField] float leftDist, rightDist;
    private float curreLeftDist, currRightDist;
    [SerializeField] float followDist, attackDist;
    private bool moveFront = true;
    private float playerDist;

    private void Start()
    {
        animator.speed = 0.3f;
        curreLeftDist = leftDist;
        currRightDist = rightDist;
    }

    // private void Update()
    // {
    //     if (player.isPlayerDye || ((npcDialogue != null) && (npcDialogue.isDialogueAllow))) return;

    //     if (weapon != null)
    //     {
    //         weapon.localPosition = Vector3.zero;
    //         weapon.localRotation = Quaternion.Euler(0, 0, -90);
    //         grab.localPosition = new Vector3(0, -0.503f, 0);
    //         grab.localRotation = Quaternion.Euler(0, 0, 0);
    //     }
    // }

    public void Move()
    {
        playerDist = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (playerDist < followDist)
        {
            if (playerDist < attackDist)
            {
                //  Debug.Log("Attack !!!");
            }
            else
            {
                //  Debug.Log("Follow !!!");
                curreLeftDist = currRightDist = player.transform.position.x;
                Walk();
            }
        }
        else
        {
            // Debug.Log("Petrol !!!");
            currRightDist = rightDist;
            curreLeftDist = leftDist;
            Walk();
        }
    }

    private void Walk()
    {
        // Move
        if (transform.localPosition.x > currRightDist) moveFront = false;     // Move Back
        else if (transform.position.x < curreLeftDist) moveFront = true;  // Move Front


        rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -10, 10), Mathf.Clamp(rigidBody.velocity.y, -10, 12));
        // Move
        if (moveFront) // Move Front
        {
            animator.Play("Walk");
            rigidBody.AddForce(Vector2.right * moveSpeed);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else // Move Back
        {
            animator.Play("Walk Back");
            rigidBody.AddForce(Vector2.right * -moveSpeed);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(leftDist, transform.localPosition.y + 4, 0), 1);
        Gizmos.DrawSphere(new Vector3(rightDist, transform.localPosition.y + 4, 0), 1);
    }
}