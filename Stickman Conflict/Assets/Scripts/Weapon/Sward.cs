using UnityEngine;
using TMPro;

public class Sward : MonoBehaviour
{
    [SerializeField] float impactForce;
    [SerializeField] float damage;
    [SerializeField] GameObject impactEffect;
    [SerializeField] TMP_Text damageText;
    private PlatformAnimals platformAnimals;
    private float applideDamage;
    private bool isCollisionAllow = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(PlayerMovement.weaponRotation, 0, 0) * Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 6, 18), ForceMode2D.Impulse);

        if (collision.gameObject.layer == 9 && isCollisionAllow)
        {
            isCollisionAllow = false;
            applideDamage = (int)Mathf.Clamp(collision.relativeVelocity.sqrMagnitude * impactForce, 5, 30);

            if (collision.gameObject.GetComponent<SpriteRenderer>().color == Color.white) damageText.color = Color.yellow;
            else damageText.color = collision.gameObject.GetComponent<SpriteRenderer>().color;
            damageText.text = applideDamage.ToString();
            Destroy(Instantiate(damageText.gameObject, new Vector3(Random.Range(collision.transform.position.x - 1, collision.transform.position.x + 1), collision.transform.position.y + 1, 0), collision.transform.rotation), 1);

            Destroy(Instantiate(impactEffect, collision.transform.position + new Vector3(0, 0.5f, 0), collision.transform.rotation), 2);
            platformAnimals = collision.gameObject.GetComponent<PlatformAnimals>();
            if (platformAnimals != null) platformAnimals.TakeDamage(applideDamage);
            Invoke("ActiveColision", 0.2f);
        }

        return;
    }

    private void ActiveColision()
    {
        isCollisionAllow = true;
    }
}