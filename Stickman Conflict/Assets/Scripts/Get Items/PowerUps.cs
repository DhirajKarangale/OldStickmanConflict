using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] ParticleSystem effect;
    private ParticleSystem currEffect;
    [SerializeField] Color effectColor;
    [SerializeField] float healthIncreaseAmount;
    private bool isHealthIncreaseAllow;

    private void Start()
    {
        isHealthIncreaseAllow = !rigidBody.isKinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 7) && isHealthIncreaseAllow)
        {
            currEffect = Instantiate(effect.gameObject, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            currEffect.startColor = effectColor;
            Destroy(currEffect.gameObject, 2);
            playerHealth.IncreaseHralth(healthIncreaseAmount);
            Destroy(gameObject);
        }

        if (rigidBody.isKinematic)
        {
            rigidBody.isKinematic = false;
            Invoke("AllowHealthIncrease", 0.5f);
        }
    }

    private void AllowHealthIncrease()
    {
        isHealthIncreaseAllow = true;
    }
}