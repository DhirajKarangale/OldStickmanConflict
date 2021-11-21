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

    private void Start()
    {
        animator.speed = 0.3f;
    }

    private void Update()
    {
        if (isOnlyMove) Move();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0.5f) * impactForce, ForceMode2D.Force);
        }

    }

    public void Move()
    {
        // Move
        if (transform.localPosition.x > rightDist) moveFront = false;     // Move Back
        else if (transform.localPosition.x < leftDist) moveFront = true;  // Move Front

        if (moveFront)
        {
            moveFront = true;
            animator.Play("Walk");
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            moveFront = false;
            animator.Play("Walk Back");
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(leftDist, -8, 0), 1);
        Gizmos.DrawSphere(new Vector3(rightDist, -8, 0), 1);
    }
}
