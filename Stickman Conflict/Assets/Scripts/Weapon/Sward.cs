using UnityEngine;

public class Sward : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] float damage;
    [SerializeField] GameObject enemyBloodEffect;
    private EmenyHealth emenyHealth;
    private bool isCollisionAllow = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(PlayerMovement.weaponRotation, 0, 0) * Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 7, 20), ForceMode2D.Impulse);

        if (collision.gameObject.layer == 9 && isCollisionAllow)
        {
            isCollisionAllow = false;
            Destroy(Instantiate(enemyBloodEffect, collision.transform.position + new Vector3(0, 0.5f, 0), collision.transform.rotation), 2);

            emenyHealth = collision.gameObject.GetComponent<EmenyHealth>();
            if ((emenyHealth != null) && (emenyHealth.currState != EmenyHealth.State.Dead)) emenyHealth.TakeDamage((int)Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * damage, 5, 30));
            Invoke("ActiveColision", 0.5f);
        }
    }

    private void ActiveColision()
    {
        isCollisionAllow = true;
    }
}