using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] float damage;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject enemyBloodEffect;
    [SerializeField] EasyJoystick.Joystick handRotateJoystick;
    public float maxDamage = 30;
    private bool isCollisionAllow = true;

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
        float applyDamage = Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * damage, 5, maxDamage);
        Vector2 handJoystick = new Vector2(handRotateJoystick.Vertical(), handRotateJoystick.Horizontal());

        if ((collision.gameObject.layer != 6) && (collision.gameObject.layer != 11)) // If not Ground
        {
            AudioManager.instance.Play("Hit");
            Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
        }

        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            Vector2 forceDir = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().position.x - transform.position.x, 0.6f);
            forceDir = forceDir.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDir * Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 9, 25), ForceMode2D.Impulse);
        }

        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            if (handJoystick == Vector2.zero)
            {
                enemyHealth.TakeDamage((int)applyDamage / 2, -1);
            }
            else
            {
                enemyHealth.TakeDamage((int)applyDamage, -1);
            }
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
                    if (handJoystick == Vector2.zero) return;

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
                    if (handJoystick == Vector2.zero)
                    {
                        enemyHealth.TakeDamage((int)applyDamage / 2, -1);
                    }
                    else
                    {
                        enemyHealth.TakeDamage((int)applyDamage, -1);
                    }
                }
            }
            else
            {
                AudioManager.instance.Play("NPCHurt");
                Instantiate(enemyBloodEffect, collision.transform.position + new Vector3(0, 0.5f, 0), collision.transform.rotation);
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