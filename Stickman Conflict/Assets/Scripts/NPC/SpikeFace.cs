using UnityEngine;

public class SpikeFace : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float jumpForce;
    [SerializeField] EnemyHealth enemyHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyHealth && (enemyHealth.currState == EnemyHealth.State.Dead))
        {
            this.enabled = false;
            return;
        }

        if(collision.gameObject.layer == 6)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}