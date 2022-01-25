using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    [Header("Refrence")]
    public Transform player;
    [SerializeField] EnemyAttack enemyAttack;
    public Animator animator;
    public Rigidbody2D rigidBody;

    [Header("Attributes")]
    [SerializeField] float moveSpeed;
    public float leftDist, rightDist;
    [SerializeField] float followDist, attackDist;
    private bool moveFront = true;

    private void Start()
    {
        if (animator) animator.speed = 0.3f;
    }

    private void Update()
    {
        if (attackDist == -1) Move();
    }

    public void Move()
    {
        float playerDist = Mathf.Abs(Vector2.Distance(transform.position, player.position));
        if ((playerDist < followDist) && !PlayerHealth.isPlayerDye)
        {
            if (playerDist >= attackDist) // Follow Player
            {
                Walk(player.position.x, player.position.x);
            }
            else // Attack
            {
                if (enemyAttack) enemyAttack.Attack();
            }
        }
        else // Petrol
        {
            Walk(rightDist, leftDist);
        }
    }

    private void Walk(float currRightDist, float curreLeftDist)
    {
        // Move
        if (transform.position.x > currRightDist) moveFront = false;     // Move Back
        else if (transform.position.x < curreLeftDist) moveFront = true;  // Move Front


        rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -10, 10), Mathf.Clamp(rigidBody.velocity.y, -10, 12));
        // Move
        if (moveFront) // Move Front
        {
            if (animator) animator.Play("Walk");
            rigidBody.AddForce(Vector2.right * moveSpeed);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else // Move Back
        {
            if (animator) animator.Play("Walk Back");
            rigidBody.AddForce(Vector2.right * -moveSpeed);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(leftDist, (transform.position.y + 4), 0), 1);
        Gizmos.DrawSphere(new Vector3(rightDist, (transform.position.y + 4), 0), 1);
    }
}