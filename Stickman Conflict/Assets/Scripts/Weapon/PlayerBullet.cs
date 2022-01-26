using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] float damage;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject enemyBloodEffect;
    public float maxDamage = 30;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            Vector2 forceDir = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().position.x - transform.position.x, 0.6f);
            forceDir = forceDir.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDir * Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 9, 25), ForceMode2D.Impulse);
        }

        float applyDamage = Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * damage, 5, maxDamage);

        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
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

                if ((collision.gameObject.name == "head"))
                {
                    if (applyDamage >= maxDamage) enemyHealth.TakeDamage((int)applyDamage * 2, 2);
                    else if (applyDamage >= (maxDamage / 2)) enemyHealth.TakeDamage((int)applyDamage * 2, 1);
                    else enemyHealth.TakeDamage((int)applyDamage * 2, 0);
                }
                else
                {
                    enemyHealth.TakeDamage((int)applyDamage, -1);
                }
            }
        }

        Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}