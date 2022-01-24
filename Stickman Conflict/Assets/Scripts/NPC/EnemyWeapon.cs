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
        if ((enemyHealth && (enemyHealth.currState == EnemyHealth.State.Dead)) || PlayerHealth.isPlayerDye)
        {
            this.enabled = false;
            return;
        }

        if (isAllowCollision)
        {
            if (collision.gameObject.layer == 7)
            {
                Vector2 forceDir = new Vector2(playerMovement.transform.position.x - transform.position.x, 0.7f);
                forceDir = forceDir.normalized;
                playerMovement.rigidBody.AddForce(forceDir * impactForce, ForceMode2D.Force);

                playerMovement.playerHealth.TakeDamage(damage);
                Instantiate(playerBloodEffect, collision.transform.position, Quaternion.identity);
            }

            if (destroyEffect)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
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
