using UnityEngine;
using UnityEngine.UI;

public class PlatformAnimals : MonoBehaviour
{
    private enum State {Move, Hurt, Dead}
    private State currState;

    [Header("Refrece")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Animator animator;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Rigidbody2D playerRIgidBody;

    [Header("Move")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rightDist, leftDist;
    private bool moveFront;
    private float startPosX;

    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] float health;
    private float currHealth;

    [Header("Attack")]
    [SerializeField] float impactForce;
    [SerializeField] GameObject playerBloodEffect;
    [SerializeField] float damage;

    private void Start()
    {
        currHealth = health;
        currState = State.Move;
        healthSlider.gameObject.SetActive(false);
        startPosX = transform.localPosition.x;
    }

    private void Update()
    {
        switch (currState)
        {
            case State.Move:
                Move();
                break;
            case State.Hurt:
                Hurt();
                break;
            case State.Dead:
                Died();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(currState == State.Dead) return;
        
        if (collision.gameObject.layer == 7)
        {
            playerRIgidBody.AddForce(new Vector2(transform.localScale.x, 0.5f) * impactForce, ForceMode2D.Force);
            playerHealth.TakeDamage(damage);
            Destroy(Instantiate(playerBloodEffect,collision.transform.position,Quaternion.identity),2);
        }
        return;
    }

    private void Move()
    {
        currState = State.Move;

        // Move
        if (transform.position.x > (rightDist + startPosX)) moveFront = false;     // Move Back
        else if (transform.position.x < (leftDist + startPosX)) moveFront = true;  // Move Front

        rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -10, 10), Mathf.Clamp(rigidBody.velocity.y, -10, 12));
        if (moveFront) // Move Front
        {
            rigidBody.AddForce(Vector2.right * moveSpeed);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else // Move Back
        {
            rigidBody.AddForce(Vector2.right * -moveSpeed);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        animator.Play("Walk");
    }

    private void Hurt()
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = currHealth/health;
        currState = State.Hurt;
        animator.Play("Hurt");
        Invoke("ExitHurt", 0.5f);
    }

    private void ExitHurt()
    {
        if (currHealth <= 0) currState = State.Dead;
        else currState = State.Move;
    }

    private void Died()
    {
        currState = State.Dead;
        rigidBody.velocity = Vector2.zero;
        healthSlider.gameObject.SetActive(false);
        animator.Play("Dead");
        Destroy(this.gameObject, 20);
    }

    public void TakeDamage(float damage)
    {
        if (currHealth <= 0) Died();
        else
        {
            currHealth -= damage;
            Hurt();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(leftDist + transform.localPosition.x, transform.localPosition.y + 4, 0), 1);
        Gizmos.DrawSphere(new Vector3(rightDist + transform.localPosition.x, transform.localPosition.y + 4, 0), 1);
    }
}