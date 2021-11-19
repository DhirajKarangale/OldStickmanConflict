using UnityEngine;

public class WalkNPC : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidBody;

    [Header("Attributes")]
    [SerializeField] bool isOnlyMove;
    [SerializeField] float moveSpeed;
    [SerializeField] float leftDist, rightDist;
    private bool moveFront = true;

    private void Start()
    {
        animator.speed = 0.3f;
    }

    private void Update()
    {
        if(isOnlyMove) Move();
    }

    public void Move()
    {
        // Move
        if (transform.position.x > rightDist) moveFront = false;     // Move Back
        else if (transform.position.x < leftDist) moveFront = true;  // Move Front

        if (moveFront)
        {
            moveFront = true;
            animator.Play("Walk");
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
        }
        else
        {
            moveFront = false;
            animator.Play("Walk Back");
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(leftDist, -8, 0), 1);
        Gizmos.DrawSphere(new Vector3(rightDist, -8, 0), 1);
    }
}
