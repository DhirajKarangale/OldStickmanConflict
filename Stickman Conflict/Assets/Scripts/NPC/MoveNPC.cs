using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidBody;

    [Header("Attributes")]
    [SerializeField] bool isOnlyMove;
    [SerializeField] float impactForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float leftDist, rightDist;
    private bool moveFront = true;
    private float startPosX;

    private void Start()
    {
        animator.speed = 0.3f;
        startPosX = transform.position.x;
    }

    private void Update()
    {
        if (isOnlyMove) Move();
    }

    public void Move()
    {
        // Move
        if (transform.localPosition.x > (rightDist + startPosX)) moveFront = false;     // Move Back
        else if (transform.position.x < (leftDist + startPosX)) moveFront = true;  // Move Front


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
        Gizmos.DrawSphere(new Vector3(leftDist + transform.localPosition.x, transform.localPosition.y + 4, 0), 1);
        Gizmos.DrawSphere(new Vector3(rightDist + transform.localPosition.x, transform.localPosition.y + 4, 0), 1);
    }
}
