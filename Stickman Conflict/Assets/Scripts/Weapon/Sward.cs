using UnityEngine;

public class Sward : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] float damage;
    public float maxDamage = 30;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject enemyBloodEffect;
    private bool isCollisionAllow = true;
    private float applyDamage;

    private void Update()
    {
        if (PlayerHealth.isPlayerDye) return;

        if (transform.position.y < -100)
        {
            transform.position += new Vector3(10, 110, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCollisionAllow) return;
        applyDamage = Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * damage, 5, maxDamage);

        if (collision.gameObject.layer != 6)
        {
            AudioManager.instance.Play("Hit");
            Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
        }

        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(PlayerMovement.weaponRotation, 0, 0) * Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 9, 25), ForceMode2D.Impulse);
        }

        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            enemyHealth.TakeDamage((int)applyDamage, -1);
        }

        if (collision.gameObject.layer == 9)
        {
            Balance balance = collision.gameObject.GetComponent<Balance>();
            if (balance)
            {
                enemyHealth = balance.enemyHealth;
                if (enemyHealth)
                {

                    if (enemyHealth.currState != EnemyHealth.State.Dead)
                    {
                        AudioManager.instance.Play("NPCHurt");
                        Instantiate(enemyBloodEffect, collision.transform.position + new Vector3(0, 0.5f, 0), collision.transform.rotation);
                    }

                    if (collision.gameObject.name == "head")
                    {
                        if (applyDamage >= maxDamage)
                        {
                            enemyHealth.TakeDamage((int)applyDamage * 2, 2);
                        }
                        else if (applyDamage >= (maxDamage / 2))
                        {
                            enemyHealth.TakeDamage((int)applyDamage * 2, 1);
                        }
                        else
                        {
                            enemyHealth.TakeDamage((int)applyDamage * 2, 0);
                        }
                    }
                    else
                    {
                        enemyHealth.TakeDamage((int)applyDamage, -1);
                    }
                }
                else
                {
                    AudioManager.instance.Play("NPCHurt");
                    Instantiate(enemyBloodEffect, collision.transform.position + new Vector3(0, 0.5f, 0), collision.transform.rotation);
                }
            }
        }

        isCollisionAllow = false;
        Invoke("ActiveColision", 0.5f);
    }

    private void ActiveColision()
    {
        isCollisionAllow = true;
    }
}