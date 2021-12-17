using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject playerBloodEffect, destroyEffect;
    [SerializeField] float damage, impactForce;
    private bool isAllowCollision = true;

    private void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyHealth && (enemyHealth.currState == EnemyHealth.State.Dead))
        {
            this.enabled = false;
            return;
        }

        if (isAllowCollision)
        {
            if (collision.gameObject.layer == 7)
            {
                if (enemyHealth)
                {
                    playerMovement.rigidBody.AddForce(new Vector2(enemyHealth.transform.localScale.x, 0.5f) * impactForce, ForceMode2D.Force);
                }
                else
                {
                    playerMovement.rigidBody.AddForce(new Vector2(transform.localScale.x, 0.5f) * impactForce, ForceMode2D.Force);
                }
                playerMovement.playerHealth.TakeDamage(damage);
                Instantiate(playerBloodEffect, collision.transform.position, Quaternion.identity);
                if (enemyHealth == null) Destroy(this.gameObject);
            }
            else
            {
                if (enemyHealth == null)
                {
                    Instantiate(destroyEffect, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }

            isAllowCollision = false;
            Invoke("AllowCollision", 0.5f);
        }
    }

    private void AllowCollision()
    {
        isAllowCollision = true;
    }
}
