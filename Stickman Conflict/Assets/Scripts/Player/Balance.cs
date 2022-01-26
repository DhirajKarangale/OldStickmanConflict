using UnityEngine;
using System.Collections;

public class Balance : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    public EnemyHealth enemyHealth;
    [SerializeField] float force;
    [SerializeField] float targetRotation;
    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if ((PlayerHealth.isPlayerDye && this.transform.parent.name == "Player") || (enemyHealth && (enemyHealth.currState == EnemyHealth.State.Dead)))
        {
            enabled = false;
            return;
        }
        rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, targetRotation, force * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyHealth) return;

        if (collision.gameObject.CompareTag("SpikeFace")) return;
        if (collision.gameObject.layer == 9)
        {
            Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDir = new Vector2(enemyRB.position.x - rigidBody.position.x, 0.5f);
            forceDir = forceDir.normalized;
            StartCoroutine(Collisions(enemyRB.GetComponent<Collider2D>()));
            enemyRB.AddForce(forceDir * 2, ForceMode2D.Impulse);
        }
    }

    IEnumerator Collisions(Collider2D enemyCollider)
    {
        Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);
    }
}