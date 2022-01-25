using UnityEngine;

public class SpikeFace : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float jumpForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}