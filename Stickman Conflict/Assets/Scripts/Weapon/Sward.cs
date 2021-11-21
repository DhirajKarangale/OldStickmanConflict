using UnityEngine;

public class Sward : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] int damage;
    [SerializeField] GameObject impactEffect;
    private PlatformAnimals platformAnimals;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(PlayerMovement.weaponRotation, 0, 0) * Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 6, 18), ForceMode2D.Impulse);
        }

        if (collision.gameObject.layer == 9)
        {
          //  Destroy(Instantiate(impactEffect, collision.gameObject.transform.position, collision.gameObject.transform.rotation), 2);
           // platformAnimals = collision.gameObject.GetComponent<PlatformAnimals>();
          //  if(platformAnimals != null) platformAnimals.TakeDamage(damage);
        }

        return;
    }
}