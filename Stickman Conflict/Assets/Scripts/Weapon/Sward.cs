using UnityEngine;

public class Sward : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] float damage;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject enemyBloodEffect;
    private EnemyHealth enemyHealth;
    private Balance balance;
    private bool isCollisionAllow = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCollisionAllow) return;

        if (collision.gameObject.layer != 6)
        {
            AudioManager.instance.Play("Hit");
            Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
        }

        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(PlayerMovement.weaponRotation, 0, 0) * Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 9, 25), ForceMode2D.Impulse);
        }

        enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            enemyHealth.TakeDamage((int)Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * damage, 5, 30));
        }

        if (collision.gameObject.layer == 9)
        {
            AudioManager.instance.Play("NPCHurt");
            Instantiate(enemyBloodEffect, collision.transform.position + new Vector3(0, 0.5f, 0), collision.transform.rotation);

            balance = collision.gameObject.GetComponent<Balance>();
            if (balance)
            {
                enemyHealth = balance.enemyHealth;
                if (enemyHealth)
                {
                    enemyHealth.TakeDamage((int)Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * damage, 5, 30));
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