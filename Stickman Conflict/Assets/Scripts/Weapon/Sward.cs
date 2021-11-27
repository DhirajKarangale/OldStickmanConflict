using UnityEngine;

public class Sward : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] float damage;
    private EmenyHealth emenyHealth;
    private float applideDamage;
    private bool isCollisionAllow = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(PlayerMovement.weaponRotation, 0, 0) * Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 7, 20), ForceMode2D.Impulse);

        if (collision.gameObject.layer == 9 && isCollisionAllow)
        {
            isCollisionAllow = false;
            applideDamage = (int)Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * damage, 5, 30);

            emenyHealth = collision.gameObject.GetComponent<EmenyHealth>();
            if ((emenyHealth != null) && (emenyHealth.currState != EmenyHealth.State.Dead)) emenyHealth.TakeDamage(applideDamage);
            Invoke("ActiveColision", 0.5f);
        }
    }

    private void ActiveColision()
    {
        isCollisionAllow = true;
    }
}