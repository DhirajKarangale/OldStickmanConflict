using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float explodeTime = 3, radius, applyDamage;
    [SerializeField] float explosionForce = 700;
    [SerializeField] GameObject explosionEffect, enemyBloodEffect;

    private void Start()
    {
        Invoke("Explode", explodeTime);
    }

    private void Explode()
    {
        Destroy(Instantiate(explosionEffect, transform.position, transform.rotation), 5);
        CamManager.Instance.Shake(10, 0.5f);

        // Add Force & Damage
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D nearByObj in colliders)
        {
            // Add Force
            Rigidbody2D objRB = nearByObj.GetComponent<Rigidbody2D>();
            if (objRB != null)
            {
                Vector2 dir = objRB.position - (Vector2)transform.position;
                if (objRB.gameObject.layer == 7) // If player
                {
                    objRB.AddForce(dir * 1000);
                }
                else
                {
                    objRB.AddForce(dir * explosionForce);
                }
            }
            // Add Damage
            Damage(nearByObj);
        }

        Destroy(gameObject);
    }

    private void Damage(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            if (enemyHealth.currState != EnemyHealth.State.Dead)
            {
                AudioManager.instance.Play("NPCHurt");
                Instantiate(enemyBloodEffect, collision.transform.position + new Vector3(0, 0.5f, 0), collision.transform.rotation);
            }
            enemyHealth.TakeDamage((int)applyDamage, -1);
        }
        else if (collision.gameObject.layer == 9)
        {
            enemyHealth = collision.gameObject.GetComponent<Balance>().enemyHealth;
            if (enemyHealth)
            {
                if (enemyHealth.currState != EnemyHealth.State.Dead)
                {
                    AudioManager.instance.Play("NPCHurt");
                    Instantiate(enemyBloodEffect, collision.transform.position + new Vector3(0, 0.5f, 0), collision.transform.rotation);
                }
                enemyHealth.TakeDamage((int)applyDamage, -1);
            }
        }
    }
}