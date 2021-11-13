using UnityEngine;

public class Sward : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] GameObject impactEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-Mathf.Sign(PlayerMovement.weaponRotation.z), 0, 0) * impactForce, ForceMode2D.Impulse);
            Destroy(Instantiate(impactEffect, collision.gameObject.transform.position, collision.gameObject.transform.rotation), 3);
        }
    }
}