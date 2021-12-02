using UnityEngine;

public class DeathPlatform : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerHealth.isPlayerDye) return;
        if (collision.gameObject.layer == 7)
        {
            playerHealth.Died();
        }
    }
}
