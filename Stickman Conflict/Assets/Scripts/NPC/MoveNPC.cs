using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Transform player;
    [SerializeField] EnemyAttack enemyAttack;
    public Animator animator;
    public Rigidbody2D rigidBody;

    [Header("Attributes")]
    [SerializeField] float moveSpeed;
    [SerializeField] float leftDist, rightDist;
    [SerializeField] float followDist, attackDist;
    private bool moveFront = true;
    private float curreLeftDist, currRightDist;

    private void Start()
    {
        animator.speed = 0.3f;
        curreLeftDist = leftDist;
        currRightDist = rightDist;
    }

    private void Update()
    {
        if (attackDist == -1) Move();
    }

    public void Move()
    {
        float playerDist = Mathf.Abs(player.position.x - transform.position.x);
        if (playerDist < followDist)
        {
            if (playerDist >= attackDist) // Follow Player
            {
                curreLeftDist = currRightDist = player.position.x;
                Walk();
            }
            else // Attack
            {
                enemyAttack.Attack();
            }
        }
        else // Petrol
        {
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
        Gizmos.DrawSphere(new Vector3(leftDist, (transform.localPosition.y + 4), 0), 1);
        Gizmos.DrawSphere(new Vector3(rightDist, (transform.localPosition.y + 4), 0), 1);
    }
}