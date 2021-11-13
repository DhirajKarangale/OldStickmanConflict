using UnityEngine;

public class WalkNPC : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Transform head;

    [Header("Attributes")]
    [SerializeField] float moveSpeed;
    [SerializeField] float leftDist, rightDist;
    private bool moveFront = true;

    private void Start()
    {
        animator.speed = 0.4f;
    }

    private void Update()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(leftDist, -8, 0), 1);
        Gizmos.DrawSphere(new Vector3(rightDist, -8, 0), 1);
    }
}
