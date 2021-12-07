using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float throwForce;
    private bool isForceAllow = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && isForceAllow)
        {
            AudioManager.instance.Play("BigJump");
            isForceAllow = false;
            playerRB.AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);
            Invoke("ActiveForce", 5);
        }
    }

    private void ActiveForce()
    {
        isForceAllow = true;
    }
}
