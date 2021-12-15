using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject playerBloodEffect, destroyEffect;
    [SerializeField] float damage, impactForce;

    private void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((enemyManager != null) &&( enemyManager.currState == EnemyManager.State.Dead)) return;

        if (collision.gameObject.layer == 7)
        {
            playerMovement.rigidBody.AddForce(new Vector2(transform.localScale.x, 0.5f) * impactForce, ForceMode2D.Force);
            playerMovement.playerHealth.TakeDamage(damage);
            Instantiate(playerBloodEffect, collision.transform.position, Quaternion.identity);
            if (enemyManager == null) Destroy(this.gameObject);
        }
        else
        {
            if (enemyManager == null)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        return;
    }
}
