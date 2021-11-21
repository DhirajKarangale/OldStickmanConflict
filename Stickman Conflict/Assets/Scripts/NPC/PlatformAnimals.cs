using UnityEngine;

public class PlatformAnimals : MonoBehaviour
{
    [SerializeField] float impactForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x,0.5f) * impactForce, ForceMode2D.Force);
        }

    }
}
