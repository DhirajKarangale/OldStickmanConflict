using UnityEngine;

public class Sward : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] GameObject impactEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            float force = Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 6, 18);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(PlayerMovement.weaponRotation, 0, 0) * force, ForceMode2D.Impulse);
        }

        if (collision.gameObject.layer == 9)
        {
            Destroy(Instantiate(impactEffect, collision.gameObject.transform.position, collision.gameObject.transform.rotation), 2);
        }

        return;
    }
}